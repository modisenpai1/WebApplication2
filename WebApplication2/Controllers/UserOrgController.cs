using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Domain.DTOs;
using WebApplication2.Domain.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOrgController : ControllerBase
    {
        private readonly IUserOrgService _repo;
        private readonly IMapper _mapper;

        public UserOrgController(IUserOrgService repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult Create(UserOrgCreateDto userOrgCreate)
        {
            var userOrg = _mapper.Map<UserOrg>(userOrgCreate);
            _repo.AddUserOrg(userOrg);
            return NoContent();
        }
        [HttpPatch]
        public IActionResult Patch(int id, JsonPatchDocument<UserOrg> patchDoc)
        {
            var userOrgFromRepo = _repo.GetUserOrg(id);
            if (userOrgFromRepo == null)
            {
                return NotFound();
            }
            var userOrgToPatch = userOrgFromRepo;
            patchDoc.ApplyTo(userOrgToPatch, ModelState);
            if (!TryValidateModel(userOrgToPatch))
            {
                return ValidationProblem(ModelState);
            }
            //_mapper.Map(userOrgToPatch, userOrgFromRepo);
            _repo.Update(userOrgFromRepo);
            return NoContent();
        }
    }
}
