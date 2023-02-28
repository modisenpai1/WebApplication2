using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
     [HttpGet]
        [Produces(typeof(CountryReadDto))]
       public IActionResult getAllCountries() {
            var Countries = _mapper.Map<IEnumerable< CountryReadDto>>(_repo.GetAllCountries());
            return Ok(Countries);
        }
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
        [HttpPatch("{id}")]
        public IActionResult PatchCity(int id, JsonPatchDocument<CountryCreateDto> patchDoc)
        {
            var CountryFromRepo = _repo.GetCountry(id);
            if (CountryFromRepo == null)
            {
                return NotFound();
            }
            var CountryToPatch = _mapper.Map<CountryCreateDto>(CountryFromRepo);
            patchDoc.ApplyTo(CountryToPatch, ModelState);
            if (!TryValidateModel(CountryToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(CountryToPatch, CountryFromRepo);
            _repo.Update(CountryFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            var Country= _repo.GetCountry(id);
       
            
                _repo.Delete(Country);
                _repo.SaveChanges();
                return NoContent();
            
        }

    }



    
}
