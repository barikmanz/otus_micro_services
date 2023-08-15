using System.Data.SqlClient;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace UserService.Infrastructure.SeedWork.Exceptions.ExceptionDescriptors
{
    public sealed class UpdateConflictDescriptor : IExceptionDescriptor
    {
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.Conflict;

        public bool CanHandle(Exception ex)
        {
            if (ex is DbUpdateException && ex.InnerException is SqlException sqlException)
            {
                const int uniqueIndexConflict = 2601;
                const int constraintConflict = 547;

                var result = sqlException.Number == uniqueIndexConflict || sqlException.Number == constraintConflict;
                return result;
            }

            return false;
        }

        public ErrorResult Handle(Exception ex)
        {
            var errors = new[]
            {
                new ErrorProperty(nameof(HttpStatusCode.Conflict), "Обновите страницу и попробуйте еще раз.")
            };

            return new ErrorResult(errors);
        }
    }
}
