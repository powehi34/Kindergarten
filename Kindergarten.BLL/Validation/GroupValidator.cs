using Entities;
using FluentValidation;

namespace Kindergarten.BLL.Validation
{
    public class GroupValidator : AbstractValidator<Group>
    {
        public GroupValidator()
        {
            RuleFor(g => g.Name).NotNull().NotEmpty();
            RuleFor(g => g.CreationYear).Must(y => y > 2000);
        }
    }
}
