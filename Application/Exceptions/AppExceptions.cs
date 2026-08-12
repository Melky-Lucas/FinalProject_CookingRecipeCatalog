using Application.Exceptions.Base;
using FluentValidation.Results;

namespace Application.Exceptions
{
    public class ConflictException : AppException
    {
        public ConflictException(string message) 
            : base(message, 409)
        {
        }

    }

    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string message) 
            : base(message, 401)
        {
        }
    }

    public class NotFoundException : AppException
    {
        public NotFoundException(string entityName, object key)
            : base($"Entity \"{entityName}\" ({key}) was not found.", 404)
        {
        }
    }

    public class AppValidationException : AppException
    {
        public IDictionary<string, string[]> Errors { get; }

        public AppValidationException(IEnumerable<ValidationFailure> failures)
            : base("One or more errors have occurred.", 422)
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }
}
