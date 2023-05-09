using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Extensions;
using UserService.Api.Models;
using UserService.Application.Handlers.Commands.CreateUserCommandHandler;
using UserService.Application.Handlers.Commands.DeleteUserCommandHandler;
using UserService.Application.Handlers.Commands.UpdateUserCommandHandler;
using UserService.Application.Handlers.Queries.GetUserQueryHandler;

namespace UserService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator) => _mediator = mediator;

    [HttpGet("ping")]
    public Task<Response<string>> Ping() => Task.FromResult(new Response<string>(DateTime.Now.ToString()));

    [HttpPost]
    public Task<Response<Guid>> Create([FromBody] CreateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = _mediator.SendWithResponse<CreateUserCommand, Guid>(command, cancellationToken);
        return result;
    }

    [HttpPut("{userId:guid}")]
    public Task<Response<Unit>> Update([FromQuery] Guid userId, [FromBody] UpdateUserDto updateUserDto,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateUserCommand()
        {
            UserId = userId,
            Email = updateUserDto.Email,
            Phone = updateUserDto.Phone,
            FirstName = updateUserDto.FirstName,
            LastName = updateUserDto.LastName
        };

        var result = _mediator.SendWithResponse<UpdateUserCommand, Unit>(command, cancellationToken);
        return result;
    }

    [HttpDelete("{userId:guid}")]
    public Task<Response<Unit>> Delete([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand()
        {
            UserId = userId
        };
        var result = _mediator.SendWithResponse<DeleteUserCommand, Unit>(command, cancellationToken);
        return result;
    }

    [HttpGet("{userId:guid}")]
    public Task<Response<GetUserByIdResult>> GetById([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery()
        {
            Id = userId
        };
        var result = _mediator.SendWithResponse<GetUserByIdQuery, GetUserByIdResult>(query, cancellationToken);
        return result;
    }
}