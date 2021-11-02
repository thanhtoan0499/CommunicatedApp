using AutoMapper;
using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Extensions;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        //private readonly UserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;  

        public UsersController(IUserRepository userRepository, IMapper mapper, DataContext dataContext)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _dataContext = dataContext;
        }
        [Authorize(Roles = "MEMBER")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();
            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(usersToReturn);
        }
        [Authorize(Roles ="MEMBER")]
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser (string username)
        {
            var user = await _userRepository.GetMemberAsync(username.ToLower());
            return Ok(user);
        }
    }
}
