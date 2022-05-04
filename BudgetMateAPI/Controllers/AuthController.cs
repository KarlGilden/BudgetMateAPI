using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetMateAPI.Data;
using BudgetMateAPI.Dtos;
using BudgetMateAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetMateAPI.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IBudgetMateRepo _repository;
           
        public AuthController(IBudgetMateRepo repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto userDto)
        {
            User user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
            };
            User u = _repository.Create(user);
            return Ok(u);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            User u = _repository.GetByEmail(loginDto.Email);
            if(u == null)
            {
                return BadRequest(new { message = "Email and password combination is invalid" });
            }

            if(!BCrypt.Net.BCrypt.Verify(loginDto.Password, u.Password))
            {
                return BadRequest(new { message = "Email and password combination is invalid" });
            }
            return Ok(u);
        }
    }
}
