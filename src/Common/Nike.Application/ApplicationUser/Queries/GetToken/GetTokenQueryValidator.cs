using FluentValidation;

namespace Nike.Application.ApplicationUser.Queries.GetToken
{
    public class GetTokenQueryValidator : AbstractValidator<GetTokenQuery>
    {
        public GetTokenQueryValidator()
        {
            RuleFor(v => v.UserName)
                .MaximumLength(100).WithMessage("User name must not exceed 100 characters.")
                .NotEmpty().WithMessage("User name is required.");

            RuleFor(v => v.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
