using Challenge.DTO;
using Challenge.Models;
using Challenge.Repository;
using FluentValidation;

namespace Challenge.Validator
{
    public class MedicalRecordValidator : AbstractValidator<TMedicalRecordDTO>
    {
        private readonly IStatusRepository _statusRepository;

        public MedicalRecordValidator(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;

            RuleFor(x => x.Diagnosis)
                .NotEmpty().WithMessage("Diagnosis is required")
                .MaximumLength(100).WithMessage("Diagnosis must not exceed 100 characters");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start Date is required")
                .LessThan(x => x.EndDate).WithMessage("Start Date must be less than End Date");

            RuleFor(x => x.StatusId)
                .NotNull().WithMessage("Status is required")
                .MustAsync((statusID, cancellationToken) => BeValidStatus(statusID, cancellationToken))
                .WithMessage("Invalid Status");

            RuleFor(x => x.FileId).NotNull().WithMessage("File is required");
        }

        private async Task<bool> BeValidStatus(int? statusId, CancellationToken cancellationToken)
        {
            var status = await _statusRepository.GetById(statusId);
            return status != null;
        }
    }
}
