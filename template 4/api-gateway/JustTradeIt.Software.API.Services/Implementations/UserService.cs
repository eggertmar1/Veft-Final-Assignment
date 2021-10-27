using System;
using System.Collections.Generic;
using AutoMapper;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Services.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDto GetUserInformation(string identifier)
        {
            return _userRepository.GetProfileInformation(identifier);
        }

        public IEnumerable<TradeDto> GetUserTrades(string userIdentifier)
        {
            // FIXME: Error here 
            throw new NotImplementedException();
            // return _userRepository.GetUserInformation(userIdentifier);
        }
    }
}