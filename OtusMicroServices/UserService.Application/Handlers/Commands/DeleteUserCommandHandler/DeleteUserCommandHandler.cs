using MediatR;
using UserService.Domain.Users.Repository;
using UserService.Infrastructure.SeedWork.UnitOfWork;

namespace UserService.Application.Handlers.Commands.DeleteUserCommandHandler;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        _userRepository.Delete(request.UserId);
        await _unitOfWork.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}