using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Domain.Models;
using WebApplication2.Services;
using WebApplication2.Domain.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Duende.IdentityServer.Extensions;

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


        [HttpGet]
        [Produces(typeof(CityReadDto))]
        public IActionResult getAllcities()
        {
            var citiesBefor = _repo.GetAll();
            var cities = _mapper.Map<IEnumerable<CityReadDto>>(citiesBefor);
            return Ok(cities);
        }


        [Produces(typeof(CityReadDto))]
        [HttpGet("{id}")]
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
        [HttpPatch("{id}")]
        public IActionResult PatchCity(int id,JsonPatchDocument<CityCreateDto>patchDoc)
        {
            var cityFromRepo= _repo.GetCity(id);
            if(cityFromRepo==null)
            {
                return NotFound();
            }
            var cityToPatch=_mapper.Map<CityCreateDto>(cityFromRepo);
            patchDoc.ApplyTo(cityToPatch, ModelState);
            if (!TryValidateModel(cityToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(cityToPatch, cityFromRepo);
            _repo.Update(cityFromRepo);
            return NoContent();
        }

    
    [HttpDelete("{id}")]
    public IActionResult DeleteCity(int id)
    {
            var city = _repo.GetCity(id);
            if(city.eventsAtCity.Count()>0 || city.UserInCity.Count()>0)
            {
                var events = _mapper.Map<IEnumerable<EventReadDto>>(city.eventsAtCity);
                var users = _mapper.Map<IEnumerable<UserRefrenceDto>>(city.UserInCity);
                return BadRequest("the entity can't be deleted as it's in use in:\n" + users.ToList() + "\n" + events.ToList());
            }
            else
            {
                _repo.Delete(city);
                return NoContent();
            }
        }
    }
}
