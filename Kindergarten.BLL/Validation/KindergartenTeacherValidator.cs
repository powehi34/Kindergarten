using Entities;
using FluentValidation;

namespace Kindergarten.BLL.Validation
{
    public class KindergartenTeacherValidator : AbstractValidator<KindergartenTeacher>
    {
        public KindergartenTeacherValidator()
        {
            RuleFor(t => t.FullName).NotNull().NotEmpty();
            RuleFor(t => t.PhoneNumber).NotNull().NotEmpty().Matches("");
            RuleFor(t => t.Address).NotNull().NotEmpty();
        }
    }
}
