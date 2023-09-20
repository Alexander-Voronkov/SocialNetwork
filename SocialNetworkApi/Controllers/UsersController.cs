using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries;
using Application.Users.Queries.GetSingleUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet("byid/{id:int}")]
        public async Task<UserDto> Get([FromRoute] GetSingleUserQuery query)
        {
            var user = await _sender.Send(query);

            return user;
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

        [HttpDelete]
        public async Task<int> Delete(DeleteUserCommand command)
        {
            var deletedUserId = await _sender.Send(command);

            return deletedUserId;
        }
    }
}
