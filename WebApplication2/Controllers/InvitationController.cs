using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication2.Domain.DTOs;
using WebApplication2.Domain.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Controller]
    [Route("Invitation")]
    public class InvitationController : ControllerBase
    {
        private readonly IEventService _repo;
        private readonly IMapper _mapper;

        public InvitationController(IEventService repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //should i add a new invitation or should i just update the old one if it existed
        //invite
        [HttpPost]
        [Authorize]
        public IActionResult InviteUser(int EventId, InvitationCreateDto InvitationCreatDto)
        {
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var RUEvent = _repo.GetEventUser(EventId, RUId);
            if (EventId != InvitationCreatDto.EventId)
            {
                return BadRequest("Event id provided in the request body doesnt match the source event id");
            }
            if (RUEvent == null || RUEvent.Role > EventRole.Administrator)
            {
                return Unauthorized();
            }
            var Invitation = _mapper.Map<Invitation>(InvitationCreatDto);
            Invitation.InvitingUserId = RUId;
            _repo.AddInvitation(Invitation);
            return NoContent();
        }


        //get invitations for user
        [Produces(typeof(IEnumerable<InvitationReadDto>))]
        [HttpGet("user/received")]
        [Authorize]
        public IActionResult GetSentInvitations()
        {
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var SentInvitatoins = _repo.GetInvitationsByUser(RUId);

            return Ok(_mapper.Map<IEnumerable<InvitationReadDto>>(SentInvitatoins));
        }


        //get invitations by user
        [Produces(typeof(IEnumerable<InvitationReadDto>))]
        [HttpGet("user/sent")]
        [Authorize]
        public IActionResult GetInvitationsForUser()
        {
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var RecievedInvitatoins = _repo.GetInvitationsForUser(RUId);

            return Ok(_mapper.Map<IEnumerable<InvitationReadDto>>(RecievedInvitatoins));
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //should this be through the event controller so the url will be /events/{eventId}/invitatoins

        //get invitations for events only accessible by admins
        [Produces(typeof(IEnumerable<InvitationReadDto>))]
        [HttpGet("event/{EventId}")]
        [Authorize]
        public IActionResult GetEventInvitatoins(int EventId)
        {
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var RUEvent = _repo.GetEventUser(EventId, RUId);
            if(RUEvent==null||RUEvent.Role==EventRole.Particpant)
            {
                return Unauthorized();
            }
            var EventInvitatoins=_mapper.Map<IEnumerable<InvitationReadDto>>(_repo.GetEventInvitations(EventId));
            return Ok(EventInvitatoins);

        }


        //get invitatoin by id ???
        [Produces(typeof(InvitationReadDto))]
        [HttpGet("{InvitationId}")]
        [Authorize]
        public IActionResult GetInvitation(int InvitatoinId)
        {
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Invitatoin = _repo.GetInvitationById(InvitatoinId);
            if(Invitatoin.InvitingUserId!=RUId&&Invitatoin.InvitedUserId!=RUId)
            {
                var RUEvent = _repo.GetEventUser(Invitatoin.EventId, RUId);
                if (RUEvent == null || RUEvent.Role == EventRole.Particpant)
                {
                    return Unauthorized();
                }
            }
            Invitatoin.IsSeen = true;
            _repo.UpdateInvitation(Invitatoin);
            return Ok(_mapper.Map<InvitationReadDto>(Invitatoin));

        }
        //update the status
        //delete an invitation
        //seperate endpoint for accepting an invitation?
    }
}
