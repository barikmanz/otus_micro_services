using System.Net;
using Microsoft.EntityFrameworkCore;

namespace UserService.Infrastructure.SeedWork.Exceptions.ExceptionDescriptors
{
    public sealed class UpdateConcurrencyDescriptor : IExceptionDescriptor
    {
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.Conflict;

        public bool CanHandle(Exception ex)
        {
            return ex is DbUpdateConcurrencyException;
        }

        public ErrorResult Handle(Exception ex)
        {
            var errors = new[]
            {
                new ErrorProperty(nameof(HttpStatusCode.Conflict), @"Данные в Базе были обновлены другим пользователем. 
                Отображаемые данные в Вашей карточке устарели, по сравнению с данными в Базе и не могут быть записаны.")
            };

            return new ErrorResult(errors);
        }
    }
}
