using FluentValidation;
using ValidationException = GT.Application.Exceptions.ValidationException;

namespace GT.Application.Common;

public interface IValidationHelper
{
    Task ValidateAsync<T>(T model, CancellationToken cancellationToken = default);
}

public class ValidationHelper : IValidationHelper
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationHelper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ValidateAsync<T>(T model, CancellationToken cancellationToken = default)
    {
        var validator = _serviceProvider.GetService(typeof(IValidator<T>)) as IValidator<T>;
        if (validator == null)
            return; // No validator found

        var result = await validator.ValidateAsync(model, cancellationToken);

        var errorsDictionary = result.Errors.Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);

        if (errorsDictionary.Count != 0)
            throw new ValidationException(errorsDictionary);
    }
}