using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Registr.Data;
using Registr.Models;

namespace Registr.Controllers
{
    public class UsersController : Controller
    {
        private readonly RegistrContext _context;

        public UsersController(RegistrContext context)
        {
            _context = context;
        }

        public IActionResult Token()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Token(string email, string password)
        {
            var us = from m in _context.User
                         select m;
            User person = us.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (person is null)
            {
                return BadRequest(new {ErrorText = "Invalid username or password! Please return to previous page to enter correct data." });
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, person.Email),
                };

            var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                userEmail = email
            };

            HttpContext.Session.SetString("SessionToken", encodedJwt);
            var requestUri = new Uri($"localhost:7075/identities?api-version=2022-06-17");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
            requestMessage.Headers.Add("Authorization", encodedJwt);
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
    }
}
