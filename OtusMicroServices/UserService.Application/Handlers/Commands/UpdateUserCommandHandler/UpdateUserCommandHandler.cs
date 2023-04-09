using MediatR;

namespace UserService.Application.Handlers.Commands.UpdateUserCommandHandler;

public class UpdateUserCommandHandler: IRequestHandler<UpdateUserCommand, Unit>
{
    public Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}