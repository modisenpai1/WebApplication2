using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Domain.DTOs;
using WebApplication2.Domain.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
       
        private readonly IMapper _mapper;
        private readonly IUserService _repo;

        public UserController(IUserService repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        [Produces(typeof(IEnumerable<CountryReadDto>))]
        public IActionResult Get() 
        {
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(_repo.GetAll()));
        }
        [HttpGet("{id}")]
        [Produces(typeof(CountryReadDto))]
        public IActionResult Get(int id)
        {
            var user = _mapper.Map<UserReadDto>(_repo.GetUser(id));
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public IActionResult Create(UserCreateDto userCreatDto)
        {
            var user = _mapper.Map<User>(userCreatDto);
            _repo.AddUser(user);
            return NoContent();
        }

        [HttpPatch]
        public IActionResult Patch(int id, JsonPatchDocument<UserCreateDto> patchDoc)
        {
            var userFromRepo = _repo.GetUser(id);
            if (userFromRepo == null)
            {
                return NotFound();
            }
            var userToPatch = _mapper.Map<UserCreateDto>(userFromRepo);
            patchDoc.ApplyTo(userToPatch, ModelState);
            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(userToPatch, userFromRepo);
            _repo.Update(userFromRepo);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok();
        }

    }
}
