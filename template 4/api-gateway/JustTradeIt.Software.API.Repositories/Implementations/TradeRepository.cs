using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.Exceptions;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class TradeRepository : ITradeRepository
    {
        private readonly JustTradeItDbContext _tradeRepository;


        private readonly IMapper _mapper;
        public TradeRepository(JustTradeItDbContext tradeRepostifory, IMapper mapper)
        {
            _tradeRepository = tradeRepostifory;
            _mapper = mapper;
        }
        /// <summary> Creates a new trade with the input model </summary>
        /// <param name="trade"> The trade to create, using the inputmodel </param>
        /// <returns> The created trade </returns>
        public string CreateTradeRequest(string email, TradeInputModel trade)
        {
            var user = _tradeRepository.Users.FirstOrDefault(e => e.Email == email);
            if (user == null)
            {
                throw new ResourceNotFoundException();
            }
            // map the input model to the entity
            var tradeEntity = _mapper.Map<Trade>(trade);
            var receiver = _tradeRepository.Users.FirstOrDefault(e => e.PublicIdentifier == trade.ReceiverIdentifier);

            var date = DateTime.Now;
            tradeEntity.IssuedDate = date;
            tradeEntity.ModifiedDate = date;
            tradeEntity.SenderId = user.Id;
            tradeEntity.ReceiverId = receiver.Id;
            tradeEntity.PublicIdentifier = Guid.NewGuid().ToString();
            tradeEntity.TradeStatus = TradeStatus.Pending.ToString();
            if (tradeEntity.SenderId == tradeEntity.ReceiverId)
            {
                throw new BadRequestException("Invalid trade recipient");
            }
            
            // add the entity to the database
            var dto =_mapper.Map<TradeDto>(tradeEntity);
            _tradeRepository.Trades.Add(tradeEntity);
            _tradeRepository.SaveChanges();
            foreach (var item in trade.ItemsInTrade)
            {
                var ItemEntity = _tradeRepository.Items.FirstOrDefault(e => e.PublicIdentifier == item.Identifier);

                var tradeItem = new TradeItem() 
                {
                    ItemId = ItemEntity.Id,
                    TradeId = tradeEntity.Id,
                    UserId = user.Id,
                };
                if (tradeItem == null) {throw new BadRequestException("Item not found");}
                if (tradeItem.UserId != user.Id) {throw new BadRequestException("You do not own this item");}
                _tradeRepository.TradeItems.Add(tradeItem);
                _tradeRepository.SaveChanges();
            }
            return tradeEntity.PublicIdentifier;
        }

        /// <summary> Gets a trade by its public identifier </summary>
        /// <param name="publicIdentifier"> The public identifier of the trade </param>
        /// <returns> The trade with the given public identifier </returns>
        public TradeDetailsDto GetTradeByIdentifier(string identifier)
        {
            var trade = _tradeRepository.Trades
                .FirstOrDefault(c => c.PublicIdentifier == identifier);
            
            if (trade == null){throw new ResourceNotFoundException();}
            // map the entity to the dto
            var dto = _mapper.Map<TradeDetailsDto>(trade);
            dto.ReceivingItems = _tradeRepository.TradeItems
                .Where(c => c.TradeId == trade.Id && c.UserId == trade.SenderId)
                .Select(c => _mapper.Map<ItemDto>(c.Item));
            dto.OfferingItems = _tradeRepository.TradeItems
                .Where(c => c.TradeId == trade.Id && c.UserId == trade.ReceiverId)
                .Select(c => _mapper.Map<ItemDto>(c.Item));
            dto.Sender = _mapper.Map<UserDto>(_tradeRepository.Users.FirstOrDefault(c => c.Id == trade.SenderId));
            dto.Receiver = _mapper.Map<UserDto>(_tradeRepository.Users.FirstOrDefault(c => c.Id == trade.ReceiverId));
            return dto;
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive)
        {
            var user = _tradeRepository.Users.FirstOrDefault(e => e.Email == email);
            var trades = _tradeRepository.Trades
                .Where(c => c.ReceiverId == user.Id && c.TradeStatus == TradeStatus.Pending.ToString());
                
            if (onlyIncludeActive)
            {
                trades = trades.Where(c => c.TradeStatus == TradeStatus.Pending.ToString());
            }
            return trades.Select(c => _mapper.Map<TradeDto>(c));
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            var trades = _tradeRepository.Trades.Where(c => c.SenderId == _tradeRepository.Users.FirstOrDefault(e => e.Email == email).Id);
            return _mapper.Map<IEnumerable<TradeDto>>(trades);
        }

        public IEnumerable<TradeDto> GetUserTrades(string userIdentifier)
        {
            var user = _tradeRepository.Users.FirstOrDefault(e => e.PublicIdentifier == userIdentifier);
            var trades = _tradeRepository.Trades.Where(c => c.SenderId == user.Id || c.ReceiverId == user.Id);
            return _mapper.Map<IEnumerable<TradeDto>>(trades);
        }

      
        /// <summary> Updates a trade with the input model </summary>
        /// <param name="trade"> The trade to update, using the inputmodel </param>
        /// <returns> The updated trade </returns>  
        public TradeDetailsDto UpdateTradeRequest(string identifier, string email, string newStatus)
        {
            var trade = _tradeRepository.Trades.FirstOrDefault(c => c.PublicIdentifier == identifier);
            if (trade == null){throw new ResourceNotFoundException();}
            var user = _tradeRepository.Users.FirstOrDefault(e => e.Email == email);
            // check if the user is the sender and receiver of the trade
            if (trade.SenderId == user.Id && trade.ReceiverId == user.Id)
            {
                throw new UnauthorizedAccessException();
            }
            //convert newstatus to enum
            if(!Enum.TryParse(newStatus, out TradeStatus status))
            {
                throw new BadRequestException("Invalid trade status, allowed: " + string.Join(",", Enum.GetNames(typeof(TradeStatus))));
            }
            trade.TradeStatus = status.ToString();
            trade.ModifiedDate = DateTime.Now;
            _tradeRepository.Trades.Update(trade);
            _tradeRepository.SaveChanges();
            return _mapper.Map<TradeDetailsDto>(trade);
        }
    }
}