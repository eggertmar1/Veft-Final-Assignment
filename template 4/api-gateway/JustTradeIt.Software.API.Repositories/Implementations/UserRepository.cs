using System;
using System.Linq;
using AutoMapper;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Repositories.Helpers;
using JustTradeIt.Software.API.Repositories.Interfaces;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly JustTradeItDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(JustTradeItDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            throw new NotImplementedException();
        }

        public UserDto CreateUser(RegisterInputModel inputModel)
        {
            if (_dbContext.Users.FirstOrDefault(c => c.Email == inputModel.Email) != null)
            {
                //TODO: make resource already exists exception 
                throw new Exception();
            }
            var token = new JwtToken{Blacklisted = false};
            _dbContext.JwtTokens.Add(token);
            _dbContext.SaveChanges();
            var entity = _mapper.Map<User>(inputModel);
            entity.PublicIdentifier = Guid.NewGuid().ToString();

            entity.HashedPassword = HashHelper.HashPassword(inputModel.Password, entity.Email);
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
            return _mapper.Map<UserDto>(entity);
        } 
        

        public UserDto GetProfileInformation(string email)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserInformation(string userIdentifier)
        {
            throw new NotImplementedException();
        }

        public void UpdateProfile(string email, string profileImageUrl, ProfileInputModel profile)
        {
            throw new NotImplementedException();
        }
    }
}