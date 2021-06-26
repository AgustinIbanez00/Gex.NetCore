using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Gex.NetCore.Extensions.Mvc
{
    public class ConfigureModelBindingLocalization : IConfigureOptions<MvcOptions>
    {
        private readonly IServiceScopeFactory _serviceFactory;
        public ConfigureModelBindingLocalization(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public void Configure(MvcOptions options)
        {
            using (var scope = _serviceFactory.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var localizer = provider.GetRequiredService<IStringLocalizer>();

                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) =>
                    localizer["The value '{0}' is not valid for {1}.", x, y]);

                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor((x) =>
                    localizer["A value for the '{0}' parameter or property was not provided.", x]);

                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() =>
                    localizer["A value is required."]);

                options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() =>
                    localizer["A non-empty request body is required."]);

                options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) =>
                    localizer["The value '{0}' is not valid.", x]);

                options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() =>
                    localizer["The supplied value is invalid."]);

                options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() =>
                    localizer["The field must be a number."]);

                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) =>
                    localizer["The supplied value is invalid for {0}.", x]);

                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) =>
                    localizer["The value '{0}' is invalid.", x]);

                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor((x) =>
                    localizer["The field {0} must be a number.", x]);

                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((x) =>
                    localizer["The value '{0}' is invalid.", x]);
            }
        }
    }
}
