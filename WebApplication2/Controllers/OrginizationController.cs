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
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var RUOrg = _repo.GetOrgUser(id, RUId);
            var orgFromRepo = _repo.GetOrginzation(id);
            
            if(orgFromRepo != null)
            {
                return NotFound();
            }
            //chage the way its setup so that the admin is > participant
            if (RUOrg == null || RUOrg.Role > OrgRole.Administrator)
            {
                return Unauthorized();
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

        //orgUsers__________________________________________________________________________________________________________________________________________


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
        [HttpPatch("OrgId/users/{id}")]
        public IActionResult ChangeRole(int OrgId, string userId, JsonPatchDocument<UserOrgCreateDto> patchDoc)
        {
            //getting the id of the user who made the request
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var RUOrg = _repo.GetOrgUser(OrgId, RUId);
            var userOrgFromRepo = _repo.GetOrgUser(OrgId, userId);
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
            if (userOrgToPatch.role == OrgRole.Administrator || RUOrg.Role== OrgRole.Member)
            {
                return Forbid();
            }

            _mapper.Map(userOrgToPatch, userOrgFromRepo);

            _repo.UpdateRole(userOrgFromRepo);
            return NoContent();
        }
        
        [Authorize]
        [HttpDelete("{orgId}/users/{userId}")]
        public IActionResult DeleteUserOrg(int OrgId, string UserId)
        {

            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var RUOrg = _repo.GetOrgUser(OrgId, RUId);
            var userOrgFromRepo = _repo.GetOrgUser(OrgId, UserId);
            if (userOrgFromRepo == null)
            {
                return NotFound();
            }
            if(RUId!= UserId)
            {
                if (RUOrg == null || RUOrg.Role > userOrgFromRepo.Role)
                {
                    return Unauthorized();
                }
            }

            _repo.DeleteOrgUser(userOrgFromRepo);
            return NoContent();
        }
        // addresses +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //post only organizatoin managers can post
        [HttpPost("{orgId}/addresses")]
        [Authorize]
        public IActionResult AddAdress(int orgId, AddressCreateDto addressCreateDto)
        {
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var RUOrg = _repo.GetOrgUser(orgId, RUId);
            if(RUOrg== null || RUOrg.Role>OrgRole.Administrator)
            {
                return Unauthorized();
            }
            if (orgId != addressCreateDto.OrginizationId)
            {
                //change error messages
                return BadRequest("Organizatoin to edit doesn't match provided id");
            }
            var Address = _mapper.Map<Adress>(addressCreateDto);
            _repo.AddAddress(Address);
            return NoContent();
        }
        //read every one can read addresses of the organization as long as they are logged in
        [HttpGet("{orgId}/addresses")]
        [Authorize]
        public IActionResult GetOrgAddresses(int orgId)
        {
            var Addresses=_mapper.Map<IEnumerable<Adress>>(_repo.GetOrgAddresses(orgId));
            return Ok(Addresses);
        }
        //patch only admins can patch the addresses
        [HttpPatch("{orgId}/addresses/{addressId}")]
        [Authorize]
        public IActionResult PatchAddress(int orgId,int addressId, JsonPatchDocument<AddressCreateDto> patchDoc)
        {
            var RUId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var RUOrg = _repo.GetOrgUser(orgId, RUId);
            var AddressFromRepo=_repo.GetAddress(addressId);
            if(AddressFromRepo==null)
            {
                return NotFound();
            }
            if (RUOrg == null || RUOrg.Role > OrgRole.Administrator)
            {
                return Unauthorized();
            }
            if (AddressFromRepo.OrginizationId!=orgId)
            {
                //change error messages
                return BadRequest("Organizatoin to edit doesn't match provided id");
            }
            var AddressToPatch = _mapper.Map<AddressCreateDto>(AddressFromRepo);
            patchDoc.ApplyTo(AddressToPatch, ModelState);
            if (!TryValidateModel(AddressToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(AddressToPatch, AddressFromRepo);
            _repo.UpdateAddress(AddressFromRepo);
            return NoContent();
        }
        //delete only admins can patch 
        [HttpDelete("{orgId/Addresses/{AddressId}")]
        [Authorize]
        public IActionResult DeleteAddress(int orgId,int addressId)
        {
            var RUId=User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var RUOrg = _repo.GetOrgUser(orgId, RUId);
            var AddressFromRepo=_repo.GetAddress(addressId);
            if (RUOrg == null || RUOrg.Role > OrgRole.Administrator)
            {
                return Unauthorized();
            }
            if (orgId != AddressFromRepo.OrginizationId)
            {
                //change error messages
                return BadRequest("Organizatoin to edit doesn't match provided id");
            }
            _repo.DeleteAddress(AddressFromRepo);
            return NoContent();
        }

    }
}

