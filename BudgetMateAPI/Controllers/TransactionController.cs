using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetMateAPI.Data;
using BudgetMateAPI.Dtos;
using BudgetMateAPI.Helpers;
using BudgetMateAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetMateAPI.Controllers
{
    [Route("transaction")]
    [ApiController]
    public class TransactionController : Controller
    {

        private readonly IBudgetMateRepo _repository;
        private readonly JwtService _jwtService;

        public TransactionController(IBudgetMateRepo repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;

        }
        [HttpGet("all")]
        public IActionResult GetAllForUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                IEnumerable<Transaction> transactions = _repository.GetAllForUser(userId);

                return Ok(transactions);
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }

        [HttpPost("add")]
        public IActionResult AddTransaction(TransactionDto tDto)
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                Transaction t = new Transaction
                {
                    Name = tDto.Name,
                    Amount = tDto.Amount,
                    Date = tDto.Date,
                    UserId = userId
                };

                Transaction transaction = _repository.Create(t);

                return Ok(transaction);
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }

        [HttpGet("delete/{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                Transaction transaction = _repository.GetTransactionById(id);
                _repository.Delete(transaction);

                return Ok();
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }
}
