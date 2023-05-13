using MediatR;

namespace UserService.Application.Handlers.Commands.UpdateUserCommandHandler;

public class UpdateUserCommand : IRequest<Unit>
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}