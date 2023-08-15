using MediatR;
using UserService.Domain.Users.Repository;
using UserService.Infrastructure.SeedWork.Exceptions;

namespace UserService.Application.Handlers.Queries.GetUserQueryHandler;

public class GetUserQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResult>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByIdResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException($"Пользователем с идентификатором {request.Id} не найден");
        }

        var result = new GetUserByIdResult
        {
            Email = user.Email,
            Id = user.Id,
            Phone = user.Phone,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName
        };
        return result;
    }
}