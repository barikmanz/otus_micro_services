using MediatR;

namespace UserService.Application.Handlers.Commands.CreateUserCommnadHandler;

public class CreateUserCommand: IRequest<Guid>
{
}