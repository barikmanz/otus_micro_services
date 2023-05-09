using MediatR;
using UserService.Domain.Users;
using UserService.Domain.Users.Repository;
using UserService.Infrastructure.SeedWork.UnitOfWork;

namespace UserService.Application.Handlers.Commands.CreateUserCommandHandler;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        var user = new User(command.UserName, command.FirstName, command.LastName, command.Email, command.Phone);
        await _userRepository.CreateAsync(user, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        return user.Id.Value;
    }
}