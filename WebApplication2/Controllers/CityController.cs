using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Domain.Models;
using WebApplication2.Services;
using WebApplication2.Domain.DTOs;
namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly ICityServices _repo;
        private readonly IMapper _mapper;

        public CityController(ICityServices repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        //[HttpGet]
        //public ActionResult<IEnumerable<City>> getAllcities()
        //{
        //    var cities = _repo.GetAll();
        //    return Ok(cities);
        //}
        [Produces(typeof(CityReadDto))]
        [HttpGet("{id}", Name = "GetgetCityById")]
        public IActionResult getCityById(int id)
        {

            var CityFromRepo = _repo.GetCity(id);
            if (CityFromRepo == null)
            {
                return NotFound();
            }
            var city = _mapper.Map<CityReadDto>(CityFromRepo);
    
   
            
       

            return Ok(city);


        }
        [Produces(typeof(CityCreateDto))]
        [HttpPost]
        public IActionResult CreateCity(CityCreateDto CityDto)
        {
            var CityModle = _mapper.Map<City>(CityDto);
            

            _repo.AddCity(CityModle);
            
            return Ok(CityModle.Id);

        }
        //[HttpPut("{id}")]
        //public ActionResult<City> UpdateCountrty(int id, City city)
        //{
        //    var oldcity = _repo.GetById(id);

        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //        oldcity = city;
        //    _repo.SaveChanges();
        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public ActionResult DeleteCountry(int id)
        //{ 
        //    var city = _repo.GetById(id);
        //    if (city == null)
        //    {
        //        return NotFound();

        //    }
        //    _repo.DeleteItem(city);
        //    _repo.SaveChanges();
        //    return NoContent();
        //}

    }
}
