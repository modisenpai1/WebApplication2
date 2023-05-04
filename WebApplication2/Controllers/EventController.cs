using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Domain.Models;
using WebApplication2.Services;
using WebApplication2.Domain.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Duende.IdentityServer.Services.KeyManagement;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventService _repo;
        private readonly IMapper _mapper;

        public EventController(IEventService repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
            var events = _repo.GetAll();
            var homePageEvents = events.Where(x => x.CityId == int.Parse(RUCityId));
            homePageEvents = homePageEvents.Union(events.Where(x => x.CountryId == int.Parse(RUCountryId)).Union(events));
            return Ok(_mapper.Map<IEnumerable<EventReadDto>>(homePageEvents));
        }


        [HttpGet("{search}")]
        [Produces(typeof(EventReadDto))]
        public IActionResult SearchEvents(string name, CityRefDto cityRefDto) {
            var city = _mapper.Map<City>(cityRefDto);
            var resualts = _repo.Search(name, city);
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
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var eventModle = _mapper.Map<Event>(eventDto);


            _repo.AddEvent(eventModle);
            EventUser EventUser = new()
            {
                UserId = RUId,
                EventId = eventModle.Id,
                Role = EventRole.Creator
                //should he have a notify me on by defualt?
            };
            _repo.AddEventUser(EventUser);
            return Ok(eventModle.Id);

        }

        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult PatchEvent(int id, JsonPatchDocument<EventCreateDto> patchDoc)
        {
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var EventUser = _repo.GetEventUser(id, RUId);
            var eventFromRepo = _repo.GetEvent(id);
            if (eventFromRepo == null)
            {
                return NotFound();
            }
            if (EventUser == null || EventUser.Role > EventRole.Administrator)
            {
                return Unauthorized();
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
            if (evnt.EventUsers.Count() > 0)
            {
                return BadRequest("the entity can't be deleted as it's in use in:\n");
            }
            else
            {
                _repo.Delete(evnt);
                return NoContent();
            }
        }

        // event user -------------------------------------------------------------------------------------------------------------------
        [HttpGet("/users")]
        [Authorize]
        ///should the event users be accesible to all users or just event admins
        public IActionResult GetEventUsers(int id)
        {
            return Ok(_repo.GetEventUsers(id));
        }


        
        [HttpPost("/users")]
        [Authorize]
        public IActionResult AddEventUser(EventUserCreateDto EventUserDto)
        {
                
        }

        [HttpDelete("/users")]
        [Authorize]
        public IActionResult DeleteEventUser(int id)
        {
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

        }
        [HttpPatch("/users")]
        [Authorize]
        public IActionResult UpdateEventUser(int id, string userId, JsonPatchDocument<EventUserCreateDto> patchDoc)
        {
            /*make the event accessable based on its status as in public events will be accessed through the front page normaly private events will be only accessed if someone adds another user
             while invite only should be researched*/
            throw new NotImplementedException();
        }
    }
}

