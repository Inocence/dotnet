using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginDto loginDto) {
            if(!ModelState.IsValid) {
                return BadRequest();
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if(!result.Succeeded) {
                return Unauthorized("Username not found and/or password incorrect");
            }
            return Ok(new NewUserDto{
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) {
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var user = new AppUser{
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };
            var createUser = await _userManager.CreateAsync(user, registerDto.Password);
            if(createUser.Succeeded) {
                var userRole = await _userManager.AddToRoleAsync(user, "User");
                if(userRole.Succeeded) {
                    return Ok(
                        new NewUserDto{
                            UserName = user.UserName,
                            Email = user.Email,
                            Token = _tokenService.CreateToken(user)
                        }
                    );
                }
                return StatusCode(500, userRole.Errors);
            }
            return StatusCode(500, createUser.Errors);
        }
    }
}