using Entities;
using FluentValidation;

namespace Kindergarten.BLL.Validation
{
    public class GroupTypeValidator : AbstractValidator<GroupType>
    {
        public GroupTypeValidator()
        {
            RuleFor(t => t.Name).NotNull().NotEmpty();
        }
    }
}
