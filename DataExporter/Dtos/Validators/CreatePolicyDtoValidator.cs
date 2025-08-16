using FluentValidation;
using DataExporter.Dtos;

namespace DataExporter.Dtos.Validators
{
    public class CreatePolicyDtoValidator : AbstractValidator<CreatePolicyDto>
    {
        public CreatePolicyDtoValidator()
        {
            RuleFor(x => x.PolicyNumber).Length(8);
            RuleFor(x => x.Premium).NotNull();
            RuleFor(x => x.StartDate).NotNull();
        }
    }
}
