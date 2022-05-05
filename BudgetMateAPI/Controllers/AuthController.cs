using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetMateAPI.Data;
using BudgetMateAPI.Dtos;
using BudgetMateAPI.Helpers;
using BudgetMateAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BudgetMateAPI.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IBudgetMateRepo _repository;
        private readonly JwtService _jwtService;

        public AuthController(IBudgetMateRepo repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;

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

            var jwt = _jwtService.Generate(u.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                    HttpOnly=true,
                    SameSite = SameSiteMode.None,
                    Secure = true
            });

            return Ok(new
            {
                message="success"
            });
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                User user = _repository.GetById(userId);

                return Ok(user);
            } catch (Exception e)
            {
                return Unauthorized();
            }

        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message= "success"
            });
        }
    }
}
