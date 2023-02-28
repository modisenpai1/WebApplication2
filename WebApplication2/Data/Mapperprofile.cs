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
            CreateMap<City,CityReadDto>();
            CreateMap<Country,CountryReadDtoCity>();
            CreateMap<City, CityReadDtoCityCountry>();
            CreateMap<Country, CountryReadDto>();
            CreateMap<CountryCreateDto, Country>();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserReadDto>().ReverseMap();
            CreateMap<User, UserRefrenceDto>().ReverseMap();
            CreateMap<Event,EventReadDto>().ReverseMap();
            CreateMap<UserEventDto,EventUser>().ReverseMap();
        }
    }
}
