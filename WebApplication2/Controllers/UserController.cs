﻿using AutoMapper;
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
        private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;

        public UserController(IUserService repo, IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _repo = repo;
            _userManager = userManager;
            //_signInManager = signInManager;
        }


        [HttpGet]
        [Produces(typeof(IEnumerable<UserReadDto>))]
        public IActionResult Get() 
        {
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(_repo.GetAll()));
        }


        [HttpGet("{id}")]
        [Produces(typeof(UserReadDto))]
        public IActionResult Get(string id)
        {
            var user = _mapper.Map<UserReadDto>(_repo.GetUser(id));
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<User>(userRegisterDto);
                user.UserName = userRegisterDto.email;
                var result=await _userManager.CreateAsync(user,userRegisterDto.password);
                if(!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await _userManager.AddToRolesAsync(user, userRegisterDto.Roles);
                return Accepted();
            }
            catch(Exception ex)
            {
                return Problem($"somthing went wrong in the {nameof(Register)}  {ex}",statusCode:500);
            }
        }
        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
               
        //        var result = await _signInManager.PasswordSignInAsync(userLoginDto.email,userLoginDto.password,false,false);
        //        if (!result.Succeeded)
        //        {
        //            return Unauthorized(userLoginDto);
        //        }
        //        return Accepted();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem($"somthing went wrong in the {nameof(Register)}", statusCode: 500);
        //    }
        //}


        [HttpPatch]
        public IActionResult Patch(string id, JsonPatchDocument<UserRegisterDto> patchDoc)
        {
            var userFromRepo = _repo.GetUser(id);
            if (userFromRepo == null)
            {
                return NotFound();
            }
            var userToPatch = _mapper.Map<UserRegisterDto>(userFromRepo);
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
        public IActionResult Delete(string id)
        {
            var user=_repo.GetUser(id);
            if (user == null)
            { return NotFound(); }
            _repo.Delete(user);
            return NoContent();
        }

    }
}
