using System.ComponentModel.DataAnnotations;

using Gex.NetCore.Resources;
using Gex.NetCore.ViewModels.Validations;

using ServiceStack.FluentValidation.Attributes;

namespace Gex.NetCore.ViewModels
{
    [Validator(typeof(RegistrationViewModelValidator))]
    public class RegistrationViewModel
    {
        [Display(Name = "correo electrónico")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resource))]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "contraseña")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        [StringLength(8, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "nombre")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [Display(Name = "apellido")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [Display(Name = "ubicación")]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resource))]
        public string Location { get; set; }
    }
}
