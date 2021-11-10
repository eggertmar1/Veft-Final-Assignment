using System;
using System.Linq;
using AutoMapper;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.Exceptions;
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

        /// <summary> Authenticates a user and returns a UserDto if successful. </summary>
        /// <param name="userInput"> The user input. </param>
        /// <returns> The user dto. </returns>
        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            var user = _dbContext.Users.FirstOrDefault(u =>
                u.Email == loginInputModel.Email &&
                u.HashedPassword == HashHelper.HashPassword(loginInputModel.Password, loginInputModel.Email));
            if (user == null) { return null; }

            var token = new JwtToken();
            _dbContext.JwtTokens.Add(token);
            _dbContext.SaveChanges();
            var dto = _mapper.Map<UserDto>(user);
            dto.TokenId = token.Id;
            _dbContext.SaveChanges();
            return dto;

        }

        /// <summary> Creates a user. </summary>
        /// <param name="userInput"> The user input. </param>
        /// <returns> The user dto. </returns>
        public UserDto CreateUser(RegisterInputModel inputModel)
        {
            // Check if user already exists
            if (_dbContext.Users.FirstOrDefault(c => c.Email == inputModel.Email) != null)
            {
                throw new ResourceAlreadyExistsException();
            }
            // Create the token for the user
            var token = new JwtToken{Blacklisted = false};
            _dbContext.JwtTokens.Add(token);
            _dbContext.SaveChanges();
            // map the input model to the entity
            var entity = _mapper.Map<User>(inputModel);
            // set the public identifier
            entity.PublicIdentifier = Guid.NewGuid().ToString();
            // hash the password with the email as salt
            entity.HashedPassword = HashHelper.HashPassword(inputModel.Password, entity.Email);
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
            var dto =_mapper.Map<UserDto>(entity);
            dto.TokenId = token.Id;
            return dto;
        } 
        

        public UserDto GetProfileInformation(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u =>
                u.Email == email);
            if (user == null) { throw new ResourceNotFoundException(); }
            return _mapper.Map<UserDto>(user);
        }


        public UserDto GetUserInformation(string userIdentifier)
        {
            var user = _dbContext.Users.FirstOrDefault(u =>
                u.PublicIdentifier == userIdentifier);
            if (user == null) { throw new ResourceNotFoundException(); }
            return _mapper.Map<UserDto>(user);
        }

        /// <summary> Updates a user. </summary>
        /// <param name="userInput"> The user input. </param>
        public void UpdateProfile(string email, string profileImageUrl, ProfileInputModel profile)
        {
            var user = _dbContext.Users.FirstOrDefault(c => c.Email == email);
            if (user == null)
            {
                throw new ResourceNotFoundException();
            }
            user.ProfileImageUrl = profileImageUrl;
            user.FullName = profile.Fullname;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        } 
    }
}