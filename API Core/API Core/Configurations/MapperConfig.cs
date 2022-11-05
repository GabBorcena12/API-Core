using API_Core.Model.Data_Transfer_Objects;
using API_Core.Model.Models;
using AutoMapper;

namespace API_Core.Configurations
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<ApiUser, ApiUserDto>().ReverseMap();
            CreateMap<Ticket, CreateTicketDto>().ReverseMap();
            CreateMap<Ticket, GetTicketDto>().ReverseMap();
            CreateMap<Ticket, UpdateTicketDto>().ReverseMap();

        }
    }
}
