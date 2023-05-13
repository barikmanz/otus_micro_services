using MediatR;
using UserService.Domain.Users.Repository;
using UserService.Infrastructure.SeedWork.UnitOfWork;

namespace UserService.Application.Handlers.Commands.UpdateUserCommandHandler;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"Пользователь не найден с Id = '{request.UserId}'");
        }

        user.SetEmail(request.Email);
        user.SetPhone(request.Phone);
        user.SetFirstName(request.FirstName);
        user.SetLastName(request.LastName);

        _userRepository.Update(user);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Unit.Value;
    }
}