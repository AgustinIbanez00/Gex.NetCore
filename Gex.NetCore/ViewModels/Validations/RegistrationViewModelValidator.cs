using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace Gex.NetCore.ViewModels.Validations
{
    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationViewModelValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("La dirección de correo electrónico es obligatoria.")
                .EmailAddress().WithMessage("Es obligatorio que la dirección de correo sea válida.");
            RuleFor(p => p.Password);
        }

    }
}
