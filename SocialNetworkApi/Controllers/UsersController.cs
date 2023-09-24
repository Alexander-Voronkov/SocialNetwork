using Application.Common.Models;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries;
using Application.Users.Queries.GetSingleUser;
using Application.Users.Queries.GetUsersByUsername;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;
        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("byid/{userid:int}")]
        public async Task<UserDto> Get([FromRoute] GetSingleUserQuery query)
        {
            var user = await _sender.Send(query);

            return user;
        }

        [HttpGet("byusername/{username}")]
        public async Task<PaginatedList<UserDto>> Get([FromRoute] GetUsersByUsernameQuery query)
        {
            var users = await _sender.Send(query);

            return users;
        }

        [HttpPost]
        public async Task<int> Post(CreateUserCommand command)
        {
            var createdUserId = await _sender.Send(command);

            return createdUserId;
        }

        [HttpPut]
        public async Task<int> Put(UpdateUserCommand command)
        {
            var updatedUserId = await _sender.Send(command);

            return updatedUserId;
        }

        [HttpDelete("{Id:int}")]
        public async Task<int> Delete([FromRoute] DeleteUserCommand command)
        {
            var deletedUserId = await _sender.Send(command);

            return deletedUserId;
        }
    }
}
