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
    public class OrginizationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrgenizationServices _repo;

        public OrginizationController(IMapper mapper,IOrgenizationServices repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        [Produces(typeof(IEnumerable<UserReadDto>))]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<OrginizationReadDtos>>(_repo.getAllorginizations()));
        }
        [HttpGet("{id}")]
        [Produces(typeof(OrginizationReadDtos))]
        public IActionResult GetById(int id)
        {
            var Org = _mapper.Map<OrginizationReadDtos>(_repo.GetOrginzation(id));
            if (    Org == null)
            {
                return NotFound();
            }
            return Ok(Org);
        }
        [HttpPost]
        public IActionResult Create(OrginizationCreateDto orginizationCreateDto)
        {
            var user = _mapper.Map<Orginization>(orginizationCreateDto);
            _repo.addOrgenization(user);
            _repo.SaveChanges();
            return NoContent();
        }

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
        [HttpDelete("{id}")]
        public IActionResult DeleteOrg(int id)
        {
            var org = _repo.GetOrginzation(id);


            _repo.removeOrgenization(org);
            _repo.SaveChanges();
            return NoContent();

        }

    }
}

