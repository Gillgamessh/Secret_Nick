using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room.Configuration
{
    internal class RoomConfigurationEf : IEntityTypeConfiguration<RoomEf>
    {
        public void Configure(EntityTypeBuilder<RoomEf> builder)
        {
            #region Relations

            builder.HasKey(room => room.Id);

            builder.HasOne(room => room.Admin)
                .WithOne(user => user.AdminRoom)
                .HasForeignKey<RoomEf>(room => room.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Values Configuration

            builder.Property(room => room.Id).HasColumnType("bigint")
                .ValueGeneratedOnAdd();

            builder.Property(room => room.CreatedOn).IsRequired();

            builder.Property(room => room.ModifiedOn).IsRequired();

            builder.Property(room => room.ClosedOn).IsRequired(false);

            builder.Property(room => room.AdminId).IsRequired(false);

            builder.HasIndex(room => room.InvitationCode).IsUnique();

            builder.Property(room => room.InvitationCode).IsRequired();

            builder.Property(room => room.MinUsersLimit).IsRequired().HasDefaultValue(3);

            builder.Property(room => room.MaxUsersLimit).IsRequired().HasDefaultValue(20);

            builder.Property(room => room.MaxWishesLimit).IsRequired().HasDefaultValue(5);

            builder.Property(room => room.Name).IsRequired().HasMaxLength(40);

            builder.Property(room => room.Description).IsRequired().HasMaxLength(200);

            builder.Property(room => room.InvitationNote).IsRequired().HasMaxLength(1000);

            builder.Property(room => room.GiftExchangeDate).IsRequired();

            builder.Property(room => room.GiftMaximumBudget).IsRequired();

            #endregion
        }
    }
}