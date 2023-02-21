using AutoMapper;
using WebApplication2.Domain.Models;
using WebApplication2.Domain.DTOs;

namespace WebApplication2.Data
{
    public class Mapperprofile : Profile
    {
        public Mapperprofile()
        {
            CreateMap<CityCreateDto,City>();
            CreateMap<City,CityReadDto>();
            CreateMap<Country,CountryReadDtoCity>();
            CreateMap<City, CityReadDtoCityCountry>();
            CreateMap<Country, CountryReadDto>();
            CreateMap<CountryCreateDto,Country>();
        }
    }
}
