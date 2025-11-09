using AutoMapper;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.AutoMapper
{
    internal class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<RoomEf, Domain.Aggregate.Room.Room>();
            CreateMap<Domain.Aggregate.Room.Room, RoomEf>()
                .ForMember(dest => dest.AdminId, opt =>
                    opt.MapFrom(room => room.Users.Where(user => user.IsAdmin).Select(user => user.Id).FirstOrDefault()));
        }
    }
}
