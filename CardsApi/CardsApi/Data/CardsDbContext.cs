using CardsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CardsApi.Data
{
    public class CardsDbContext :DbContext
    {
        public CardsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
    }
}
