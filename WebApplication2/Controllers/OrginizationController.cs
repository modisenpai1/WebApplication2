using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication2.Domain.DTOs;
using WebApplication2.Domain.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrginizationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrgenizationServices _repo;

        public OrginizationController(IMapper mapper, IOrgenizationServices repo)
        {
            _mapper = mapper;
            _repo = repo;
        }



        [HttpGet]
        [Produces(typeof(IEnumerable<OrginizationReadDtos>))]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<OrginizationReadDtos>>(_repo.getAllorginizations()));
        }



        //[HttpGet]
        //[Authorize]
        //[Produces(typeof(IEnumerable<OrginizationReadDtos>))]
        //public IActionResult GetUserOrgs()
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    var user =


        //    return Ok(_mapper.Map<IEnumerable<OrginizationReadDtos>>(_repo.getAllorginizations()));
        //}



        [HttpGet("{id}")]
        [Produces(typeof(OrginizationReadDtos))]
        public IActionResult GetByIdAsync(int id)
        {
            var Org = _mapper.Map<OrginizationReadDtos>(_repo.GetOrginzation(id));
            if (Org == null)
            {
                return NotFound();
            }
            return Ok(Org);
        }



        [Authorize]
        [HttpPost]
        public IActionResult Create(OrginizationCreateDto orginizationCreateDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orginization = _mapper.Map<Orginization>(orginizationCreateDto);

            _repo.addOrgenization(orginization);
            UserOrg userOrg = new()
            {
                OrginizationId = orginization.Id,
                UserId = userId,
                Role = OrgRole.Creator
            };
            _repo.AddOrgUser(userOrg);
            _repo.SaveChanges();
            return NoContent();
        }



        [Authorize]
        [HttpPatch]
        public IActionResult Patch(int id, JsonPatchDocument<OrginizationCreateDto> patchDoc)
        {
            var orgFromRepo = _repo.GetOrginzation(id);
            if (orgFromRepo == null)
            {
                return NotFound();
            }
            var OrgToPatch = _mapper.Map<OrginizationCreateDto>(orgFromRepo);
            patchDoc.ApplyTo(OrgToPatch, ModelState);
            if (!TryValidateModel(OrgToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(OrgToPatch, orgFromRepo);
            _repo.updateorginzation(orgFromRepo);
            return NoContent();
        }



        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteOrg(int id)
        {
            var org = _repo.GetOrginzation(id);

            _repo.removeOrgenization(org);
            _repo.SaveChanges();
            return NoContent();

        }



        [Route("users")]
        [Authorize]
        [HttpPost]
        public IActionResult AddMembers(UserOrgCreateDto userOrgCreateDto)
        {
            if (userOrgCreateDto.role == OrgRole.Creator)
            {
                return Forbid();
            }
            var userOrg = _mapper.Map<UserOrg>(userOrgCreateDto);
            _repo.AddOrgUser(userOrg);
            return NoContent();
        }

        
        [Authorize]
        [HttpPatch("users/{id}")]
        public IActionResult ChangeRole(int id, string userId, JsonPatchDocument<UserOrgCreateDto> patchDoc)
        {
            //getting the id of the user who made the request
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var RUOrg = _repo.GetOrgUser(id, RUId);
            var userOrgFromRepo = _repo.GetOrgUser(id, userId);
            //checking if the requesting user has authority for the request
            if (userOrgFromRepo == null)
            {
                return NotFound();
            }
            if (RUOrg == null || RUOrg.Role < userOrgFromRepo.Role)
            {
                return Unauthorized();
            }

            var userOrgToPatch = _mapper.Map<UserOrgCreateDto>(userOrgFromRepo);

            patchDoc.ApplyTo(userOrgToPatch, ModelState);
            if (!TryValidateModel(userOrgToPatch))
            {
                return ValidationProblem(ModelState);
            }
            //temporary???
            if (userOrgToPatch.role == OrgRole.Admin)
            {
                return Forbid();
            }

            _mapper.Map(userOrgToPatch, userOrgFromRepo);

            _repo.UpdateRole(userOrgFromRepo);
            return NoContent();
        }
        
        [Authorize]
        [HttpDelete("users/{id}")]
        public IActionResult DeleteUserOrg(int id, string userId)
        {

            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var RUOrg = _repo.GetOrgUser(id, RUId);
            var userOrgFromRepo = _repo.GetOrgUser(id, userId);
            if (RUOrg == null || RUOrg.Role < userOrgFromRepo.Role)
            {
                return Unauthorized();
            }

            _repo.DeleteOrgUser(userOrgFromRepo);
            return NoContent();
        }

    }
}

