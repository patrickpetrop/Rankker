using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RankkerAPI.Data;
using RankkerAPI.Models;

namespace RankkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [Route("/register")]
        [HttpPost]
        public async Task<IActionResult> Register(string firstName, string lastName, string username, string email, string password)
        {
            var user = new AppUser()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UserName = username
            };

            var result = await _userManager.CreateAsync(user, password);

            //TODO assign default role

            return new OkObjectResult("Account created");
        }
    }
}