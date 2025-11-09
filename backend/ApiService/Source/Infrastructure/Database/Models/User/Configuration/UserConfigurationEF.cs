using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User.Configuration
{
    internal class UserConfigurationEf : IEntityTypeConfiguration<UserEf>
    {
        public void Configure(EntityTypeBuilder<UserEf> builder)
        {
            #region Relations

            builder.HasKey(user => user.Id);

            builder.HasOne(user => user.GiftRecipientUser)
                .WithOne(user => user.GiftSenderUser)
                .HasForeignKey<UserEf>(user => user.GiftRecipientUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(user => user.Room)
                .WithMany(room => room.Users)
                .HasForeignKey(user => user.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Wishes)
                .WithOne(gift => gift.User)
                .HasForeignKey(gift => gift.UserId);

            #endregion

            #region Values Configuration

            builder.Property(user => user.Id).HasColumnType("bigint")
                .ValueGeneratedOnAdd();

            builder.Property(user => user.CreatedOn).IsRequired();

            builder.Property(user => user.ModifiedOn).IsRequired();

            builder.Property(user => user.RoomId).IsRequired();

            builder.HasIndex(user => user.AuthCode).IsUnique();

            builder.Property(user => user.AuthCode).IsRequired();

            builder.Property(user => user.FirstName).HasMaxLength(40).IsRequired();

            builder.Property(user => user.LastName).HasMaxLength(40).IsRequired();

            builder.Property(user => user.Phone).IsRequired();

            builder.Property(user => user.DeliveryInfo).HasMaxLength(500).IsRequired();

            builder.Property(user => user.WantSurprise).IsRequired().HasDefaultValue(true);

            builder.Property(user => user.Interests).HasMaxLength(1000);

            #endregion
        }
    }
}