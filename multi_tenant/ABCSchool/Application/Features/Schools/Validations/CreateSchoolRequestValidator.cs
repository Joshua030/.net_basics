using FluentValidation;
using ABCSharedLibrary.Models.Requests.Schools;

namespace Application.Features.Schools.Validations
{
    internal class CreateSchoolRequestValidator : AbstractValidator<CreateSchoolRequest>
    {
        public CreateSchoolRequestValidator()
        {
            RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("School name is required")
            .MaximumLength(60);

            RuleFor(request => request.EstablishedDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Date established cannot be future date");
        }
    }
}
