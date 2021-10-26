using JustTradeIt.Software.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JustTradeIt.Software.API.Repositories.Contexts
{
    public class JustTradeItDbContext : DbContext
    {
        public JustTradeItDbContext(DbContextOptions<JustTradeItDbContext> options) :
        base(options) { }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     // manual config of relations -> many to many
        //     modelBuilder.Entity<User>()
        //         .HasKey(x => new { x.ownerId, x.Id });
        // //     modelBuilder.Entity<AuthorNewsItem>()
        // //         .HasKey(x => new { x.AuthorsId, x.NewsItemsId });
        // }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCondition> ItemConditions { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<JwtToken> JwtTokens { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradeItem> TradeItems { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}