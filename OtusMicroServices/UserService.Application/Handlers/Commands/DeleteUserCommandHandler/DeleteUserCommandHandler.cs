using MediatR;

namespace UserService.Application.Handlers.Commands.DeleteUserCommandHandler;

public class DeleteUserCommandHandler:IRequestHandler<DeleteUserCommand, Unit>
{
    public Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}