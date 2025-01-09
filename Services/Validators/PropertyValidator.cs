using Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators
{
    public class PropertyValidator : AbstractValidator<Property>
    {
        public PropertyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Name).MaximumLength(255).WithMessage("Name can not be longer than 255 characters");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.Address).MaximumLength(255).WithMessage("Address can not be longer than 255 characters");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.CodeInternal).NotEmpty().WithMessage("CodeInternal is required");
            RuleFor(x => x.CodeInternal).MaximumLength(20).WithMessage("CodeInternal can not be longer than 20 characters");
            RuleFor(x => x.Year).NotEmpty().WithMessage("Year is required");
            RuleFor(x => x.Year).GreaterThanOrEqualTo(1900).WithMessage("Year must be greater than or equal to 1900"); 
            RuleFor(x => x.Year).LessThanOrEqualTo(DateTime.Now.Year).WithMessage("Year must be less than or equal to the current year");
            RuleFor(x => x.IdOwner).NotEmpty().WithMessage("IdOwner is required");
        }
    }
}
