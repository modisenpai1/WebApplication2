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
            CreateMap<Country,CountryReadDtoCity>().ReverseMap();
            CreateMap<City, CityReadDtoCityCountry>().ReverseMap();
            CreateMap<Country, CountryReadDto>().ReverseMap();
            CreateMap<CountryCreateDto, Country>().ReverseMap();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserReadDto>().ReverseMap();
            CreateMap<User, UserRefrenceDto>().ReverseMap();
            CreateMap<Event,EventReadDto>().ReverseMap();
            CreateMap<EventCreateDto, Event>().ReverseMap();
            CreateMap<EventRefDto, Event>().ReverseMap();
            CreateMap<UserEventReadDto,EventUser>().ReverseMap();
            CreateMap<EventUserReadDto,EventUser>().ReverseMap();
            CreateMap<UserOrgCreateDto,UserOrg>().ReverseMap();
<<<<<<< HEAD
            CreateMap<AddressRefDto,Adress>().ReverseMap();
            CreateMap<OrganizationRefDto,Orginization>().ReverseMap();
            CreateMap<OrginizationReadDtos, Orginization>().ReverseMap();
            CreateMap<Orginization, OrginizationCreateDto>().ReverseMap();
=======
            CreateMap<UserOrgReadDto, UserOrg>().ReverseMap();
            CreateMap<OrginizationReadDtos, Orginization>().ReverseMap();
            CreateMap<Orginization, OrginizationCreateDto>().ReverseMap();
            CreateMap<AddressRefDto,Adress>().ReverseMap();
            CreateMap<OrganizationRefDto,Orginization>().ReverseMap();
>>>>>>> 0173e65f6814706f0256f4cd50497888d86d933b

        }
    }
}