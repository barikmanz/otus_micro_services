using MediatR;

namespace UserService.Application.Handlers.Commands.CreateUserCommnadHandler;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    public Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}