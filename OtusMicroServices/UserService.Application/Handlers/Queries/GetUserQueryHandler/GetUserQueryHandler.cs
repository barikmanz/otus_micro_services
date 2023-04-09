using MediatR;

namespace UserService.Application.Handlers.Queries.GetUserQueryHandler;

public class GetUserQueryHandler: IRequestHandler<GetUserByIdQuery,GetUserByIdResult>
{
    public Task<GetUserByIdResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}