using System.Globalization;
using AutoMapper;
using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using Epam.ItMarathon.ApiService.Api.Dto.ReadDtos;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Entities.User;

namespace Epam.ItMarathon.ApiService.Api.Dto.Mapping
{
    internal class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<RoomCreationDto, RoomApplication>()
                .ForMember(roomApplication => roomApplication.GiftExchangeDate, opt => opt
                    .MapFrom(roomDto => DateTime.Parse(
                        roomDto.GiftExchangeDate,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal
                    )));

            CreateMap<Room, RoomReadDto>().ForMember(roomDto => roomDto.AdminId,
                opt => opt.MapFrom(room => room.Users.First(user => user.IsAdmin).Id));

            CreateMap<User, UserReadDto>()
                .ForMember(dest => dest.UserCode, opt =>
                {
                    opt.PreCondition((src, context) =>
                        (bool)context.Items["AuthAsAdmin"] ||             // Auth user is Admin
                        src.AuthCode.Equals(context.Items["OwnerCode"])); // or is Owner of the record.
                    opt.MapFrom(user => user.AuthCode);
                })
                .ForMember(dest => dest.Phone, opt =>
                {
                    opt.PreCondition((src, context) =>
                        (bool)context.Items["AuthAsAdmin"] ||                 // Auth user is Admin
                        src.IsAdmin ||                                        // or this is IsAdmin record,
                        src.AuthCode.Equals(context.Items["OwnerCode"]) ||    // or is Owner of the record,
                        src.Id.Equals(context.Items["GiftRecipientUserId"])); // or this is Target record for auth user.
                    opt.MapFrom(user => user.Phone);
                })
                .ForMember(dest => dest.Email, opt =>
                {
                    opt.PreCondition((src, context) =>
                        (bool)context.Items["AuthAsAdmin"] ||                 // Auth user is Admin
                        src.IsAdmin ||                                        // or this is IsAdmin record,
                        src.AuthCode.Equals(context.Items["OwnerCode"]) ||    // or auth user is Owner of the record,
                        src.Id.Equals(context.Items["GiftRecipientUserId"])); // or this is Target record for auth user.
                    opt.MapFrom(user => user.Email ?? string.Empty);
                })
                .ForMember(dest => dest.GiftToUserId, opt =>
                {
                    opt.PreCondition((src, context) =>
                        src.AuthCode.Equals(context.Items["OwnerCode"])); // Auth user is Owner of the record.
                    opt.MapFrom(user => user.GiftRecipientUserId);
                })
                .ForMember(dest => dest.DeliveryInfo, opt =>
                {
                    opt.PreCondition((src, context) =>
                        (bool)context.Items["AuthAsAdmin"] ||                 // Auth user is Admin
                        src.AuthCode.Equals(context.Items["OwnerCode"]) ||    // or is Owner of the record,
                        src.Id.Equals(context.Items["GiftRecipientUserId"])); // or this is Target record for auth user.
                    opt.MapFrom(user => user.DeliveryInfo);
                })
                .ForMember(dest => dest.WantSurprise, opt =>
                {
                    opt.PreCondition((src, context) =>
                        src.AuthCode.Equals(context.Items["OwnerCode"]) ||    // Auth user is Owner of the record,
                        src.Id.Equals(context.Items["GiftRecipientUserId"])); // or this is Target record for auth user.
                    opt.MapFrom(user => user.WantSurprise);
                })
                .ForMember(dest => dest.Interests, opt =>
                {
                    opt.PreCondition((src, context) =>
                        src.AuthCode.Equals(context.Items["OwnerCode"]) ||    // Auth user is Owner of the record,
                        src.Id.Equals(context.Items["GiftRecipientUserId"])); // or this is Target record for auth user.
                    opt.MapFrom(user => user.Interests ?? string.Empty);
                })
                .ForMember(dest => dest.WishList, opt =>
                {
                    opt.PreCondition((src, context) =>
                        src.AuthCode.Equals(context.Items["OwnerCode"]) ||    // Auth user is Owner of the record,
                        src.Id.Equals(context.Items["GiftRecipientUserId"])); // or this is Target record for auth user.
                    opt.MapFrom(user => user.Wishes.Any()
                        ? user.Wishes.Select(wish => new WishDto { Name = wish.Name, InfoLink = wish.InfoLink })
                        : new List<WishDto>());
                });
        }
    }
}