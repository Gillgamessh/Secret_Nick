using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Gift.Configuration
{
    internal class GiftConfigurationEf : IEntityTypeConfiguration<GiftEf>
    {
        public void Configure(EntityTypeBuilder<GiftEf> builder)
        {
            builder.HasKey(gift => gift.Id);

            #region Values Configuration

            builder.Property(gift => gift.Id).HasColumnType("bigint")
                .ValueGeneratedOnAdd();

            builder.Property(gift => gift.CreatedOn).IsRequired();

            builder.Property(gift => gift.ModifiedOn).IsRequired();

            builder.Property(gift => gift.UserId).IsRequired();

            builder.Property(gift => gift.Name).HasMaxLength(40);

            #endregion
        }
    }
}