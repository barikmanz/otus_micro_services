using MediatR;

namespace UserService.Application.Handlers.Commands.DeleteUserCommandHandler;

public class DeleteUserCommand : IRequest<Unit>
{
    public Guid UserId { get; set; }
}