
using FluentValidation;
using MediatR;
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace SaBooBo.Domain.Shared.Behaviour;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Count > 0)
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Any())
        {
            var error = failures[0];

            throw new BadRequestException(
                code: error.ErrorCode,
                message: error.ErrorMessage,
                description: "Validation Error"
            );
        }

        // Return response
        return await next();
    }
}