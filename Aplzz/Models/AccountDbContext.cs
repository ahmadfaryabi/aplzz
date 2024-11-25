using Microsoft.EntityFrameworkCore;

namespace Aplzz.Models
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {

           Database.ExecuteSqlRaw("PRAGMA foreign_keys = ON;");
        
        }


        public DbSet<AccountProfile> AccountProfiles { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Assuming AccountProfileAccountId is the custom primary key
            modelBuilder.Entity<AccountProfile>()
               // .HasKey(a => a.AccountProfileAccountId)
                .HasOne(a => a.Account)  // Account is the related entity
                .WithMany()  // A single Account can have many AccountProfiles (adjust based on your model)
                .HasForeignKey(a => a.AccountId) // AccountId is the foreign key
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<AccountProfile>()
                .HasKey(a => a.AccountProfileAccountId); // Set primary key for AccountProfile if necessary

        }
    }
}