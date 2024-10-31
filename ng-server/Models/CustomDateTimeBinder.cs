using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

public class CustomDateTimeBinder : IModelBinder
{
    private const string DateFormat = "dd.MM.yyyy HH:mm";

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult != ValueProviderResult.None)
        {
            var dateValue = valueProviderResult.FirstValue;

            if (DateTime.TryParseExact(dateValue, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                bindingContext.Result = ModelBindingResult.Success(date);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid date format. Please use 'dd.MM.yyyy HH:mm'.");
            }
        }

        return Task.CompletedTask;
    }
}
