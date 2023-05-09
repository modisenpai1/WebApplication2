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
        public IActionResult JoinEvent(EventUserCreateDto EventUserDto)
        {
            var EventFromRepo = _repo.GetEvent(EventUserDto.EventId);
            var EventUser=_mapper.Map<EventUser>(EventFromRepo);
            //public 
            if(EventFromRepo.accessibality == Accessibality.Public)
            {
                if(_repo.GetEventUser(EventUser.EventId,EventUser.UserId)==null)
                {
                    _repo.AddEventUser(EventUser);
                    return NoContent();
                }
                else
                {
                    return Ok("the User has already joined the event");
                }
            }

            //Private
            if(EventFromRepo.accessibality!= Accessibality.Private)
            {
                var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var RUEvent=_repo.GetEventUser(EventUser.EventId,RUId);
                if(RUEvent!=null && RUEvent.Role < EventRole.Particpant)
                {
                    if (_repo.GetEventUser(EventUser.EventId, EventUser.UserId) == null)
                    {
                        _repo.AddEventUser(EventUser);
                        return NoContent();
                    }
                    else
                    {
                        return Ok("the User has already joined the event");
                    }
                }
            }

            //Invite
            return Unauthorized();
        }

        [HttpDelete("/users")]
        [Authorize]
        public IActionResult DeleteEventUser(int EventId,string UserId)
        {
           
            
            var eventUserFromRepo = _repo.GetEventUser(EventId,UserId);
            if (eventUserFromRepo == null)
            {
                return NotFound();
            }

            // Check if the current user has permission to delete the event user
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (RUId != UserId)
            {
                var RUEvent = _repo.GetEventUser(EventId, RUId);
                if (RUEvent == null || RUEvent.Role >= eventUserFromRepo.Role)
                {
                    return Forbid();
                }
            }

            _repo.DeleteEventUser(eventUserFromRepo);

            return NoContent();
            

        }
  
       
        [HttpPatch("/users")]
        [Authorize]
        public IActionResult UpdateEventUser(string userId, int eventId, [FromBody] JsonPatchDocument<EventUserCreateDto> patchDoc)
        {
            var eventUserFromRepo = _repo.GetEventUser(eventId, userId);
            if (eventUserFromRepo == null)
            {
                return NotFound();
            }

            var eventUserToPatch = _mapper.Map<EventUserCreateDto>(eventUserFromRepo);
            patchDoc.ApplyTo(eventUserToPatch, ModelState);

            if (!TryValidateModel(eventUserToPatch))
            {
                return ValidationProblem(ModelState);
            }

            // Check if the current user has permission to update the role
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (eventUserToPatch.Role != eventUserFromRepo.Role)
            {
                if (RUId == userId)
                {
                    return Forbid();
                }

                var RUEvent = _repo.GetEventUser(eventId, RUId);
                if (RUEvent == null || RUEvent.Role >= eventUserFromRepo.Role)
                {
                    return Unauthorized();
                }
            }

            // Only update the NotifyMe field if the current user is the target user
            if (RUId != userId)
            {
                eventUserToPatch.NotifyMe = eventUserFromRepo.NotifyMe;
            }

            _mapper.Map(eventUserToPatch, eventUserFromRepo);
            _repo.UpdateEventUser(eventUserFromRepo);

            return NoContent();
        }
        //invitations__________________________________________________________________________________________________________________

        //invite
        [HttpPost("{EventId}/Invitations")]
        [Authorize]
        public IActionResult InviteUser(int EventId,InvitationCreateDto InvitationCreatDto)
        {
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var RUEvent=_repo.GetEventUser(EventId, RUId);
            if(EventId!=InvitationCreatDto.EventId)
            {
                return BadRequest("Event id provided in the request body doesnt match the source event id");
            }
            if(RUEvent == null||RUEvent.Role>EventRole.Administrator)
            {
                return Unauthorized();
            }
            var Invitation = _mapper.Map<Invitation>(InvitationCreatDto);
            Invitation.InvitingUserId = RUId;
            _repo.AddInvitation(Invitation);
            return NoContent() ;
        }
        [HttpGet("/User/Invitations/")]
        [Authorize]
        //get invitations for user
        //get invitations by user
        //get invitations for events only accessible by admins
        //get invitatoin by id ???
        //update the status and the is seen variable
        //cancle an invitation
        //seperate endpoint for accepting an invitation?
    }
}

