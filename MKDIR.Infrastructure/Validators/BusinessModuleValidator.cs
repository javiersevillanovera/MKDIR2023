using FluentValidation;
using MKDIR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Infrastructure
{
    public class BusinessModuleValidator : AbstractValidator<BusinessModuleDTO>
    {
        public BusinessModuleValidator() 
        {
            RuleFor(u => u.Name).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El Nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El nombre no puede ser vacio.");

            RuleFor(u => u.IsOperator)
                .NotNull().WithMessage("El campo IsOperator no puede ser nulo.");

            RuleFor(u => u.Sequence)
                .NotNull().WithMessage("El campo Sequence no puede ser nulo.");

            RuleFor(u => u.Sequence).NotEqual(0).WithMessage("numero incorrecto");

            RuleFor(u => u.Icon).NotNull().WithMessage("icon es nulo");
        }    
    }
}
