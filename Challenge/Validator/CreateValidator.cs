using Challenge.Repository;
using FluentValidation;

namespace Challenge.Validator
{
    public class CreateValidator : MedicalRecordValidator
    {
        public CreateValidator(IStatusRepository statusRepository, IMedicalRecordTypeRepository medicalRecordTypeRepository)
            : base(statusRepository, medicalRecordTypeRepository)
        {
            RuleFor(x => x.StatusId)
            .Must(statusId => statusId == 1)
            .WithMessage("Cannot assign Inactive status when creating a new record");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created By is required");
        }
    }
}
