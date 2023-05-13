using MediatR;

namespace UserService.Application.Handlers.Queries.GetUserQueryHandler;

public class GetUserByIdQuery : IRequest<GetUserByIdResult>
{
    public Guid Id { get; set; }
}