﻿using BobsTacosBackend.Data;
using BobsTacosBackend.Enums;
using BobsTacosBackend.Models;
using BobsTacosBackend.Request;
using BobsTacosBackend.Response;
using BobsTacosBackend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BobsTacosBackend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _context;
        private readonly TokenService _tokenService;

        public UsersController(UserManager<ApplicationUser> userManager, DatabaseContext context,
            TokenService tokenService, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(
                new ApplicationUser
                {
                    FirstName = request.firstName,
                    LastName = request.lastName,
                    UserName = request.Username,
                    Email = request.Email,
                    Role = request.Role
                },
                request.Password!
            );

            if (result.Succeeded)
            {
                // Retrieve the newly created user
                var user = await _userManager.FindByEmailAsync(request.Email);

                // Construct the response object including the user ID
                var response = new
                {
                    Id = user.Id,  // Include the user ID in the response
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role
                };

                return CreatedAtAction(nameof(Register), response);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }




        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managedUser = await _userManager.FindByEmailAsync(request.Email!);

            if (managedUser == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password!);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var userInDb = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (userInDb is null)
            {
                return Unauthorized();
            }

            var accessToken = _tokenService.CreateToken(userInDb);
            await _context.SaveChangesAsync();

            return Ok(new AuthResponse
            {
                Id = userInDb.Id,  // Include the user ID in the response
                Username = userInDb.UserName,
                Email = userInDb.Email,
                FirstName = userInDb.FirstName,
                LastName = userInDb.LastName,
                Token = accessToken,
            });
        }

    }
}
