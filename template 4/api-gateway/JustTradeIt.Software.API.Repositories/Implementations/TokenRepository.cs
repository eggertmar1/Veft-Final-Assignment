using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.Exceptions;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Repositories.Interfaces;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        private readonly JustTradeItDbContext _dbContext;
        public TokenRepository(JustTradeItDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        ///  <summary> create a new token </summary>
        ///  <param name="token"> the token to create </param>
        ///  <returns> the created token </returns>
        public JwtToken CreateNewToken()
        {
            var newToken = new JwtToken{Blacklisted = false};
            _dbContext.JwtTokens.Add(newToken);
            _dbContext.SaveChanges();
            return newToken;
        }

        ///  <summary> get a token by id and checks if it is blacklisted</summary>
        ///  <param name="id"> the id of the token to get </param>
        ///  <returns> boolean </returns>
        public bool IsTokenBlacklisted(int tokenId)
        {
            var token = _dbContext.JwtTokens.Find(tokenId);
            if(token == null) {
                throw new ResourceNotFoundException();
            } 
            return token.Blacklisted;  
        }
        ///  <summary> set a token to blacklisted </summary>
        ///  <param name="id"> the id of the token to set </param>
        public void VoidToken(int tokenId)
        {
            var token = _dbContext.JwtTokens.Find(tokenId);
            if(token == null) {
                throw new System.Exception();
            } 
            _dbContext.JwtTokens.Remove(token);
            _dbContext.SaveChanges();
        }
    }
}