using JustTradeIt.Software.API.Models.Entities;
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

        public JwtToken CreateNewToken()
        {
            var newToken = new JwtToken{Blacklisted = false};
            _dbContext.JwtTokens.Add(newToken);
            _dbContext.SaveChanges();
            return newToken;
        }

        public bool IsTokenBlacklisted(int tokenId)
        {
            var token = _dbContext.JwtTokens.Find(tokenId);
            if(token == null) {
                throw new System.Exception();
            } 
            return token.Blacklisted;

            
        }

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