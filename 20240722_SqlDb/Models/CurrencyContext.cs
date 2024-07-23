using Microsoft.EntityFrameworkCore;

namespace _20240722_SqlDb.Models
{
    public class CurrencyContext: DbContext
    {
        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options) { }

        public DbSet<Currency>? Currencies { get; set; } = null!;
    }
}
