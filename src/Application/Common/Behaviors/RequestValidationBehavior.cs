using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            string[] failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .Select(e => e.ErrorMessage)
                .ToArray();

            if (failures.Any())
                throw new Exceptions.ValidationException(failures[0]);

            return await next();
        }
    }
}
