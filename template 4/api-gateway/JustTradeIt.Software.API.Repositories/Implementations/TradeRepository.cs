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
        private readonly JustTradeItDbContext _tradeRepostifory;


        private readonly IMapper _mapper;
        public TradeRepository(JustTradeItDbContext tradeRepostifory, IMapper mapper)
        {
            _tradeRepostifory = tradeRepostifory;
            _mapper = mapper;
        }
        public string CreateTradeRequest(string email, TradeInputModel trade)
        {
            var user = _tradeRepostifory.Users.FirstOrDefault(e => e.Email == email);
            
            var tradeEntity = _mapper.Map<Trade>(trade);
            var receiver = _tradeRepostifory.Users.FirstOrDefault(e => e.PublicIdentifier == trade.ReceiverIdentifier);
    
            var date = DateTime.Now;
            tradeEntity.IssuedDate = date;
            tradeEntity.ModifiedDate = date;
            tradeEntity.SenderId = user.Id;
            tradeEntity.ReceiverId = receiver.Id;
            tradeEntity.PublicIdentifier = Guid.NewGuid().ToString();
            tradeEntity.TradeStatus = TradeStatus.Pending.ToString();
            // tradeEntity.TradeItems = 
            var dto =_mapper.Map<TradeDto>(tradeEntity);
            _tradeRepostifory.Trades.Add(tradeEntity);
            _tradeRepostifory.SaveChanges();
            foreach (var item in trade.ItemsInTrade)
            {
                var ItemEntity = _tradeRepostifory.Items.FirstOrDefault(e => e.PublicIdentifier == item.Identifier);
                var tradeItem = new TradeItem()
                {
                    ItemId = ItemEntity.Id,
                    TradeId = tradeEntity.Id,
                    UserId = user.Id,
                };
                _tradeRepostifory.TradeItems.Add(tradeItem);
                _tradeRepostifory.SaveChanges();
            }
            return tradeEntity.PublicIdentifier;
        }
        public TradeDetailsDto GetTradeByIdentifier(string identifier)
        {
            var trade = _tradeRepostifory.Trades
                .FirstOrDefault(c => c.PublicIdentifier == identifier);
            
            if (trade == null){throw new ResourceNotFoundException();}
            var dto = _mapper.Map<TradeDetailsDto>(trade);
            dto.ReceivingItems = _tradeRepostifory.TradeItems
                .Where(c => c.TradeId == trade.Id && c.UserId == trade.SenderId)
                .Select(c => _mapper.Map<ItemDto>(c.Item));
            dto.OfferingItems = _tradeRepostifory.TradeItems
                .Where(c => c.TradeId == trade.Id && c.UserId == trade.ReceiverId)
                .Select(c => _mapper.Map<ItemDto>(c.Item));
            dto.Sender = _mapper.Map<UserDto>(_tradeRepostifory.Users.FirstOrDefault(c => c.Id == trade.SenderId));
            dto.Receiver = _mapper.Map<UserDto>(_tradeRepostifory.Users.FirstOrDefault(c => c.Id == trade.ReceiverId));
            return dto;
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive)
        {
            var trades = _tradeRepostifory.Trades.Where(c => c.ReceiverId == _tradeRepostifory.Users.FirstOrDefault(e => e.Email == email).Id);
            if (onlyIncludeActive)
            {
                trades = trades.Where(c => c.TradeStatus == TradeStatus.Pending.ToString());
            }
            return _mapper.Map<IEnumerable<TradeDto>>(trades);
            

            // var dto = _mapper.Map<IEnumerable<TradeDto>>(trades);
   
            // return dto;

            // var items = _dbContext.Items.Where(i => i.Deleted == false).ToList();
            // var bleh = items.Select( item =>
            // {
            //     var dto = _mapper.Map<ItemDto>(item);
            //     dto.Owner = _mapper.Map<UserDto>(_dbContext.Users.Find(item.OwnerId));
            //     return dto;
            // });
            throw new NotImplementedException();
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            var trades = _tradeRepostifory.Trades.Where(c => c.SenderId == _tradeRepostifory.Users.FirstOrDefault(e => e.Email == email).Id);
            return _mapper.Map<IEnumerable<TradeDto>>(trades);
        }

        public IEnumerable<TradeDto> GetUserTrades(string userIdentifier)
        {
            throw new NotImplementedException();
        }

        public TradeDetailsDto UpdateTradeRequest(string email, string identifier, Models.Enums.TradeStatus newStatus)
        {
            throw new NotImplementedException();
        }
    }
}