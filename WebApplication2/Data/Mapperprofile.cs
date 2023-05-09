using AutoMapper;
using WebApplication2.Domain.Models;
using WebApplication2.Domain.DTOs;

namespace WebApplication2.Data
{
    public class Mapperprofile : Profile
    {
        public Mapperprofile()
        {
            CreateMap<CityCreateDto,City>().ReverseMap();
            CreateMap<City,CityReadDto>().ReverseMap();
            CreateMap<City, CityRefDto>().ReverseMap();

            CreateMap<Country,CountryRefDto>().ReverseMap();
            CreateMap<Country, CountryReadDto>().ReverseMap();
            CreateMap<CountryCreateDto, Country>().ReverseMap();

            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserReadDto>().ReverseMap();
            CreateMap<User, UserRefDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();

            CreateMap<Event,EventReadDto>().ReverseMap();
            CreateMap<EventCreateDto, Event>().ReverseMap();
            CreateMap<EventRefDto, Event>().ReverseMap();

            CreateMap<UserEventReadDto,EventUser>().ReverseMap();
            CreateMap<EventUserReadDto,EventUser>().ReverseMap();

            CreateMap<Invitation,InvitationReadDto>().ReverseMap();
            CreateMap<Invitation,InvitationCreateDto>().ReverseMap();

            CreateMap<OrganizationRefDto,Orginization>().ReverseMap();
            CreateMap<OrginizationReadDtos, Orginization>().ReverseMap();
            CreateMap<Orginization, OrginizationCreateDto>().ReverseMap();

            CreateMap<UserOrgCreateDto,UserOrg>().ReverseMap();
            CreateMap<UserOrgReadDto, UserOrg>().ReverseMap();
            CreateMap<OrgUserReadDto, UserOrg>().ReverseMap();


            CreateMap<AddressRefDto,Adress>().ReverseMap();
        }
    }
}