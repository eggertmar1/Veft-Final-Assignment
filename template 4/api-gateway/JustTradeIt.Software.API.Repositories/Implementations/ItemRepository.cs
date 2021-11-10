using System;
using System.Linq;
using AutoMapper;
using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.Exceptions;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly JustTradeItDbContext _dbContext;
        private readonly IMapper _mapper;


        public ItemRepository(JustTradeItDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary> creates new item </summary>
        /// <param name="item"> item to create </param>
        /// <returns> created item </returns>
        public string AddNewItem(string email, ItemInputModel item)
        {
            var user = _dbContext.Users.FirstOrDefault( c => c.Email == email);
            if (user == null) {throw new Exception("User does not exist");}
            if(item == null) {throw new Exception("Item does not exist");}
            var conditionCode = _dbContext.ItemConditions.FirstOrDefault(c => c.ConditionCode == item.ConditionCode);
            if (conditionCode == null){
                conditionCode = new ItemCondition 
                {
                    ConditionCode = item.ConditionCode,
                    Description = ""
                };
                _dbContext.Add(conditionCode);
                _dbContext.SaveChanges();
            } 


            var entity = _mapper.Map<Item>(item);
            entity.PublicIdentifier = Guid.NewGuid().ToString();
            entity.ItemCondition= conditionCode;
            entity.OwnerId = user.Id;
            
            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            foreach(var image in item.ItemImages) {
                _dbContext.ItemImages.Add(new ItemImage 
                {
                    ImageUrl = image,
                    ItemId = entity.Id,
                });
            }
            _dbContext.SaveChanges();
            return entity.PublicIdentifier;
        }

        /// <summary> gets item by public identifier </summary>
        /// <param name="publicIdentifier"> public identifier of item </param>
        /// <returns> item </returns>
        public Envelope<ItemDto> GetAllItems(int pageSize, int pageNumber, bool ascendingSortOrder)
        {
            var items = _dbContext.Items.Where(i => i.Deleted == false).ToList();
            var bleh = items.Select( item =>
            {
                var dto = _mapper.Map<ItemDto>(item);
                dto.Owner = _mapper.Map<UserDto>(_dbContext.Users.Find(item.OwnerId));
                return dto;
            });
            if (items == null) {throw new ResourceNotFoundException();}
            bleh = ascendingSortOrder ? bleh.OrderBy(i => i.Title) : bleh.OrderByDescending(i => i.Title);
            return new Envelope<ItemDto>(pageNumber, pageSize, bleh);
        }

        /// <summary> gets item by public identifier </summary>
        /// <param name="publicIdentifier"> public identifier of item </param>
        /// <returns> item </returns>
        public ItemDetailsDto GetItemByIdentifier(string identifier)
        {
            var item = _dbContext.Items
                .FirstOrDefault( c => c.PublicIdentifier == identifier);
            if(item == null) {throw new ResourceNotFoundException();}
            if(item.Deleted) {throw new ResourceNotFoundException("Item has been deleted");}
            var dto = _mapper.Map<ItemDetailsDto>(item);
            dto.Owner = _mapper.Map<UserDto>(_dbContext.Users.Find(item.OwnerId));
            dto.Images = _dbContext.ItemImages.Where( c => c.ItemId == item.Id).Select( t => _mapper.Map<ImageDto>(t));
            dto.Condition = _dbContext.ItemConditions.Find(item.ItemConditionId).ConditionCode;
            return dto;

        }
        /// <summary> gets item by public identifier </summary>
        /// <param name="publicIdentifier"> public identifier of item </param>
        /// <returns> item </returns>
        public void RemoveItem(string email, string identifier)
        {
            var item = _dbContext.Items.FirstOrDefault( c => c.PublicIdentifier == identifier);
            if(item == null) {throw new ResourceNotFoundException();}   
            if(item.OwnerId != _dbContext.Users.FirstOrDefault( c => c.Email == email).Id) {throw new ForbiddenException("User does not own item");}
            // check if item has been traded 
            if(_dbContext.TradeItems.Any( c => c.ItemId == item.Id)) {throw new ForbiddenException("Item has been traded");}
            item.Deleted = true;
            _dbContext.SaveChanges();
        }
    }
}