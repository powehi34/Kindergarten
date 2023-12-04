using Entities;
using FluentValidation;

namespace Kindergarten.BLL.Validation
{
    public class ChildValidator : AbstractValidator<Child>
    {
        public ChildValidator()
        {
            RuleFor(c => c.FullName).NotNull().NotEmpty();
            RuleFor(c => c.MotherFullName).NotNull().NotEmpty();
            RuleFor(c => c.FatherFullName).NotNull().NotEmpty();
            RuleFor(c => c.BirthDate).NotNull().NotEmpty();
            RuleFor(c => c.Sex).Must(s => s == 0 || s == 1);
        }
    }
}
