using AutoMapper;
using WebApplication2.Domain.Models;
using WebApplication2.Domain.DTOs;

namespace WebApplication2.Data
{
    public class Mapperprofile : Profile
    {
        public Mapperprofile()
        {
            CreateMap<City, CityCreateDto>().ReverseMap();
            CreateMap<City,CityReadDto>().ReverseMap();
            CreateMap<City, CityRefDto>().ReverseMap();

            CreateMap<Country,CountryRefDto>().ReverseMap();
            CreateMap<Country, CountryReadDto>().ReverseMap();
            CreateMap<Country,CountryCreateDto>().ReverseMap();

            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserReadDto>().ReverseMap();
            CreateMap<User, UserRefDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();

            CreateMap<Event,EventReadDto>().ReverseMap();
            CreateMap<Event,EventCreateDto>().ReverseMap();
            CreateMap<Event,EventRefDto>().ReverseMap();
            CreateMap<Event, EventSearchDto>().ReverseMap();

            CreateMap<EventUser, UserEventReadDto>().ReverseMap();
            CreateMap<EventUser, EventUserReadDto>().ReverseMap();

            CreateMap<Invitation,InvitationReadDto>().ReverseMap();
            CreateMap<Invitation,InvitationCreateDto>().ReverseMap();

            CreateMap<Orginization, OrganizationRefDto>().ReverseMap();
            CreateMap<Orginization, OrginizationReadDtos>().ReverseMap();
            CreateMap<Orginization, OrginizationCreateDto>().ReverseMap();
            CreateMap<Orginization,OrginizationSearchDto>().ReverseMap();

            CreateMap<UserOrg, UserOrgCreateDto>().ReverseMap();
            CreateMap<UserOrg, UserOrgReadDto>().ReverseMap();
            CreateMap<UserOrg, OrgUserReadDto>().ReverseMap();


            CreateMap<Adress, AddressRefDto>().ReverseMap();
            CreateMap<Adress, AddressCreateDto>().ReverseMap();
            CreateMap<Adress, AddressReadDto>().ReverseMap();
        }
    }
}