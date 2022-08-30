using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Entities;
using PasswordManager.Models;
using PasswordManager.Services.interfaces;
using System;

namespace PasswordManager.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("[action]/{username}/{password}")]
        public async Task<UserDto> Signin(string username, string password, CancellationToken cancellationToken)
        {
            var result = await _userService.Signin(username, password, cancellationToken);
            if (result != null)
            {
  var tt = _mapper.Map<UserDto>(result);
                return _mapper.Map<UserDto>(result);
            }
              
            else
                return null;
        }

        [HttpGet]
        public async Task<List<UserDto>> GetAll(CancellationToken cancellationToken)
        {
            return _mapper.Map<List<UserDto>>(await _userService.GetAll(cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<UserDto> Get(int id, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserDto>(await _userService.GetById(id, cancellationToken));
        }
        [HttpPost]
        public async Task<UserDto> AddUser(UserDto userDto, CancellationToken cancellationToken)
        {
            var newUser = _mapper.Map<User>(userDto);
            newUser.Id = 0;

            return _mapper.Map<UserDto>(await _userService.Add(newUser, cancellationToken));
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteUser(int id, CancellationToken cancellationToken)
        {
            await _userService.Delete(id, cancellationToken);
            return true;
        }
        [HttpPut("{id}")]
        public async Task<bool> UpdateUser(int id, [FromBody] User user, CancellationToken cancellationToken)
        {
            await _userService.Update(id, user, cancellationToken);
            return true;
        }
    }
}
