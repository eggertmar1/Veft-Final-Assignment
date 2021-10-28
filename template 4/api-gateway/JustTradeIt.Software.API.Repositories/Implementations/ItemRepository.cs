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

        public string AddNewItem(string email, ItemInputModel item)
        {
            var user = _dbContext.Users.FirstOrDefault( c => c.Email == email);
            if (user == null) {throw new Exception("User does not exist");}
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
            entity.Identifier = new Guid().ToString();
            entity.ItemConditionId = conditionCode.Id;
            entity.OwnerId = user.Id;
            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            foreach(var image in entity.ItemImages) {
                _dbContext.ItemImages.Add(new ItemImage 
                {
                ImageUrl = image.ImageUrl,
                ItemId = entity.Id

                });
            }
            _dbContext.SaveChanges();
            return entity.Identifier;



            



        }


        // {
        //     if (_dbContext.Users.FirstOrDefault(c => c.Email == inputModel.Email) != null)
        //     {
        //         throw new ResourceAlreadyExistsException();
        //     }
        //     var token = new JwtToken{Blacklisted = false};
        //     _dbContext.JwtTokens.Add(token);
        //     _dbContext.SaveChanges();
        //     var entity = _mapper.Map<User>(inputModel);
        //     entity.PublicIdentifier = Guid.NewGuid().ToString();

        //     entity.HashedPassword = HashHelper.HashPassword(inputModel.Password, entity.Email);
        //     _dbContext.Users.Add(entity);
        //     _dbContext.SaveChanges();
        //     var dto =_mapper.Map<UserDto>(entity);
        //     dto.TokenId = token.Id;
        //     return dto;
        // } 
        




        public Envelope<ItemDto> GetAllItems(int pageSize, int pageNumber, bool ascendingSortOrder)
        {
            throw new NotImplementedException();
        }

        public ItemDetailsDto GetItemByIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(string email, string identifier)
        {
            throw new NotImplementedException();
        }
    }
}