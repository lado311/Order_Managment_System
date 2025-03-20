using AuthApi.Application.DTOs;
using AuthApi.Domain.Entities;
using AuthApi.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagment.SharedLibrary.Exceptions;
using System.Linq.Expressions;

namespace AuthApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUser userRepository, IMapper mapper) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> UserRegistration(UserAddDto userAddDto)
        {
            var user = mapper.Map<User>(userAddDto);
            var result = await userRepository.CreateAsync(user);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserAuthorization(UserLoginDto loginDto)
        {
            var user = mapper.Map<User>(loginDto);
            string token = await userRepository.LoginUser(user);
            if (string.IsNullOrEmpty(token)) throw new BadRequestException("something wrong, token not generated!");

            return Ok($"user succesfully authorization, token: {token}");
        }

        [HttpGet("get-user/{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user is null) throw new NotFoundRequestException($"user not found by id: {id}");

            return Ok(mapper.Map<UserGetDto>(user));
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userRepository.GetAllAsync();
            return Ok(mapper.Map<List<UserGetDto>>(users));
        }

        [HttpPut("update-user/{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, UserAddDto userAddDto)
        {
            var userFromDataBase = await userRepository.GetByIdAsync(id);
            if (userFromDataBase is null) throw new NotFoundRequestException($"user not found by id: {id}");

            mapper.Map(userAddDto, userFromDataBase);

            var result = await userRepository.UpdateAsync(userFromDataBase);
            return Ok(result);
        }

        [HttpDelete("delete-user/{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userFromDatabase = await userRepository.GetByIdAsync(id);
            if (userFromDatabase is null) throw new NotFoundRequestException($"user not found following id: {id}");

            var result = await userRepository.DeleteAsync(userFromDatabase);
            return Ok($"delete process successfully ended, deleted user: {result.Email}");
        }
    }
}
