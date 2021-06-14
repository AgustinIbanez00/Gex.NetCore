using FluentValidation;

namespace Gex.NetCore.ViewModels.Validations
{
    public class CredentialsViewModelValidator : AbstractValidator<CredentialsViewModel>
    {
        public CredentialsViewModelValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("La dirección de correo electrónico es obligatoria.")
                .EmailAddress().WithMessage("Es obligatorio que la dirección de correo sea válida.");
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Debes ingresar una contraseña.").Length(6, 12)
                .WithMessage("La contraseña debe contener entre 6 y 12 caracteres.");
        }
    }
}
