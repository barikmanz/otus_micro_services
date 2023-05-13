using MediatR;
using UserService.Api.Models;

namespace UserService.Api.Extensions;

public static class MediatrExtensions
{
    public static async Task<Response<TResult>> SendWithResponse<TCommand, TResult>(
        this IMediator mediator, TCommand command, CancellationToken cancellationToken)
        where TCommand : IRequest<TResult>
    {
        var result = await mediator.Send(command, cancellationToken);

        return new Response<TResult>(result);
    }
}