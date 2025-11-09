using System.Diagnostics.CodeAnalysis;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Gift;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Gift.Configuration;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room.Configuration;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database
{
    [ExcludeFromCodeCoverage]
    internal class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UserEf> Users { get; set; }
        public DbSet<GiftEf> Gifts { get; set; }
        public DbSet<RoomEf> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoomConfigurationEf());
            modelBuilder.ApplyConfiguration(new UserConfigurationEf());
            modelBuilder.ApplyConfiguration(new GiftConfigurationEf());
        }
    }
}