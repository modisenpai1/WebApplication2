using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Domain.DTOs;
using WebApplication2.Domain.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{//scafholder Needs AutoMapper
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICountryServices _repo;

        public CountryController(ICountryServices repo,IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        //[HttpGet]
        //public ActionResult<IEnumerable<Country>> getAllCountries() {
        //    var Countries = _repo.GetAll();
        //    return Ok(Countries);
        //}
        [HttpGet("{id}", Name = "GetgetCountryById")]
        public ActionResult<Country> getCountryById(int id) {

         
            if (_repo.GetCountry(id) == null)
            {
                return BadRequest();
            }
            var CountryFromRepo = _repo.GetCountry(id);
            var country=_mapper.Map<CountryReadDto>(CountryFromRepo);
      
            return Ok(country);


        }
        [HttpPost]
        public ActionResult<Country> CreateCountrty(CountryCreateDto country)
        {
            var CountryModle = _mapper.Map<Country>(country);
           _repo.AddCountry(CountryModle);
            return Ok();

        }
        //[HttpPut("{id}")]
        //public ActionResult<Country> UpdateCountrty(int id, Country country)
        //{
        //    var OldCountry = _repo.GetById(id);

        //    if (country == null)
        //    {
        //        return NotFound();
        //    }
        //    OldCountry = country;
        //    _repo.SaveChanges();
        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public ActionResult DeleteCountry(int id) {
        //    var Country = _repo.GetById(id);
        //    if (Country == null)
        //    {
        //        return NotFound();

        //    }
        //    _repo.DeleteItem(Country);
        //    _repo.SaveChanges();
        //    return NoContent();
        //}

            
        }



    
}
