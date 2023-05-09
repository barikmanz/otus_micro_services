using MediatR;

namespace UserService.Application.Handlers.Commands.CreateUserCommandHandler;

public class CreateUserCommand: IRequest<Guid>
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
}