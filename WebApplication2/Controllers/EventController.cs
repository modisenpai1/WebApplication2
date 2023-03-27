using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Domain.Models;
using WebApplication2.Services;
using WebApplication2.Domain.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventService _repo;
        private readonly IMapper _mapper;

        public EventController(IEventService repo,IMapper mapper) 
        { 
            _repo=repo;
            _mapper=mapper;
        }
        [HttpGet]
        [Produces(typeof(EventReadDto))]
        public IActionResult getAllEvents()
        {
            var events = _mapper.Map<IEnumerable<EventReadDto>>(_repo.GetAll());
            return Ok(events);
        }


        [Authorize]
        [HttpGet("homepage")]
        [Produces(typeof(EventReadDto))]
        public IActionResult HomePage()
        {
            var RUCityId = User.FindFirst("City").Value;
            var RUCountryId = User.FindFirst(ClaimTypes.Country).Value;
            var events= _repo.GetAll();
            var homePageEvents=events.Where(x => x.CityId == int.Parse(RUCityId));
            homePageEvents.Union(events.OrderBy(x => x.CountryId == int.Parse(RUCountryId)));
            return Ok(_mapper.Map<EventReadDto>(homePageEvents));
        }


        [HttpGet("{search}")]
        [Produces(typeof(EventReadDto))]
        public IActionResult SearchEvents(string name,  CityRefDto cityRefDto) {
            var city = _mapper.Map<City>(cityRefDto);
            var resualts = _repo.Search(name,city);
            if (resualts.Any())
            {
                return Ok(_mapper.Map<IEnumerable<EventReadDto>>(resualts));
            }
            else { return NotFound(); }
        }


        [Produces(typeof(EventReadDto))]
        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {

            var eventFromRepo = _repo.GetEvent(id);
            if (eventFromRepo == null)
            {
                return NotFound();
            }
            var evnt = _mapper.Map<EventReadDto>(eventFromRepo);

            return Ok(evnt);
        }

        [Authorize]
        [Produces(typeof(EventCreateDto))]
        [HttpPost]
        public IActionResult CreateEvent(EventCreateDto eventDto)
        {
            var eventModle = _mapper.Map<Event>(eventDto);


            _repo.AddEvent(eventModle);
          

            return Ok(eventModle.Id);

        }
        [HttpPatch("{id}")]
        public IActionResult PatchEvent(int id, JsonPatchDocument<EventCreateDto> patchDoc)
        {
            var eventFromRepo = _repo.GetEvent(id);
            if (eventFromRepo == null)
            {
                return NotFound();
            }
            var eventToPatch = _mapper.Map<EventCreateDto>(eventFromRepo);
            patchDoc.ApplyTo(eventToPatch, ModelState);
            if (!TryValidateModel(eventToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(eventToPatch, eventFromRepo);
            _repo.Update(eventFromRepo);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var evnt = _repo.GetEvent(id);
            if (evnt.EventUsers.Count()>0)
            {
                return BadRequest("the entity can't be deleted as it's in use in:\n" );
            }
            else
            {
                _repo.Delete(evnt);
                return NoContent();
            }
        }
    }
}

