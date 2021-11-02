using AutoMapper;
using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> userManager;

        public AccountController(DataContext context, ITokenService tokenService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
            this.userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register (RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is tasken");
            var user = _mapper.Map<AppUser>(registerDto);  
            user.SecurityStamp = Guid.NewGuid().ToString();

            user.UserName = registerDto.Username.ToLower();

            IdentityResult result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            IdentityResult roleResult = await userManager.AddToRoleAsync(user, "MEMBER");
            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);
           
            return new UserDto 
            { 
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpPost("LoginDto")]
        public async Task<ActionResult<UserDto>> Login (LoginDto loginDto)
        {
            var user = await _context.AppUsers.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            if (user == null) return Unauthorized("Invalid username");
          
            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExists (string username)
        {
           return await userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
