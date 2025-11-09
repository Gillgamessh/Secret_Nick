using AutoMapper;
using Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Gift;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.AutoMapper
{
    internal class GiftMappingProfile : Profile
    {
        public GiftMappingProfile()
        {
            CreateMap<Wish, GiftEf>()
                .ForMember(giftEf => giftEf.CreatedOn, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(giftEf => giftEf.ModifiedOn, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<GiftEf, Wish>();
        }
    }
}