using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Extensions;
using UserService.Api.Models;
using UserService.Application.Handlers.Commands.CreateUserCommnadHandler;
using UserService.Application.Handlers.Commands.UpdateUserCommandHandler;

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

    [HttpPut]
    public Task<Response<Unit>> Update([FromBody] UpdateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = _mediator.SendWithResponse<UpdateUserCommand, Unit>(command, cancellationToken);
        return result;
    }
}