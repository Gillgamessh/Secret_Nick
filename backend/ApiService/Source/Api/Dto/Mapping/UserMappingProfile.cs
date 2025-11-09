using AutoMapper;
using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using Epam.ItMarathon.ApiService.Api.Dto.Responses.UserResponses;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using Epam.ItMarathon.ApiService.Domain.Entities.User;

namespace Epam.ItMarathon.ApiService.Api.Dto.Mapping
{
    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserCreationDto, UserApplication>()
                .ForMember(userApplication => userApplication.Wishes, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    if (src.WishList is not null)
                    {
                        dest.Wishes = src.WishList.Select(wish => (wish.Name, wish.InfoLink)).ToList();
                    }
                });

            CreateMap<User, UserCreationResponse>()
                .ForMember(dest => dest.UserCode, opt => opt.MapFrom(user => user.AuthCode))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(user => user.Email ?? string.Empty))
                .ForMember(dest => dest.Interests, opt => opt.MapFrom(user => user.Interests ?? string.Empty))
                .ForMember(dest => dest.WishList, opt =>
                {
                    opt.MapFrom(user => user.Wishes.Any()
                        ? user.Wishes.Select(wish => new WishDto { Name = wish.Name, InfoLink = wish.InfoLink })
                        : new List<WishDto>());
                });
        }
    }
}