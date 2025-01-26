using Challenge.DTO;
using Challenge.Repository;
using FluentValidation;

namespace Challenge.Validator
{
    public class DeleteValidator : AbstractValidator<TMedicalRecordDTO>
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        public DeleteValidator(IStatusRepository statusRepository, IMedicalRecordRepository medicalRecordRepository)
        {
            _statusRepository = statusRepository;
            _medicalRecordRepository = medicalRecordRepository;

            RuleFor(x => x.DeletionReason)
               .NotEmpty().WithMessage("Deletion Reason is required")
               .MaximumLength(2000).WithMessage("Deletion Reason must not exceed 2000 characters");

            RuleFor(x => x.DeletedBy)
                .NotEmpty().WithMessage("Deleted By is required");

            RuleFor(x => x.StatusId)
                .MustAsync((statusID, cancellationToken) => BeValidStatus(statusID, cancellationToken));
        }

        private async Task<bool> BeValidStatus(int? statusID, CancellationToken cancellationToken)
        {
            var status = await _statusRepository.GetById(statusID);
            if (status == null)
                return false;

            return !status.Name!.Equals("Inactive");
        }
    }
}
