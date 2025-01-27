using Challenge.DTO;
using Challenge.Repository;
using FluentValidation;

namespace Challenge.Validator
{
    public class UpdateValidator : MedicalRecordValidator
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IStatusRepository _statusRepository;

        public UpdateValidator(IStatusRepository statusRepository,
            IMedicalRecordTypeRepository medicalRecordTypeRepository,
            IMedicalRecordRepository medicalRecordRepository) 
            : base(statusRepository, medicalRecordTypeRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _statusRepository = statusRepository;

            RuleFor(x => x.ModifiedBy)
            .NotEmpty().WithMessage("Modified By is required");

            When(x => x.StatusId == 2, () => {
                RuleFor(x => x.DeletionReason)
                    .NotEmpty().WithMessage("Deletion Reason is required when changing to Inactive status")
                    .MaximumLength(2000);

                RuleFor(x => x.EndDate)
                    .NotEmpty().WithMessage("End Date is required when changing to Inactive status");
            });

            RuleFor(x => x)
                .MustAsync(BeValidForUpdate)
                .WithMessage("Medical Record does not exist or is already Inactive");

            When(x => x.EndDate.HasValue && !x.StatusId.HasValue, () => {
                RuleFor(x => x.StatusId)
                    .Equal(2)
                    .WithMessage("Status must be set to Inactive when End Date is provided");
            });
        }

        private async Task<bool> BeValidForUpdate(TMedicalRecordDTO dto, CancellationToken token)
        {
            var existingRecord = await _medicalRecordRepository.GetMedicalRecordById(dto.MedicalRecordId);
            if (existingRecord == null) return false;

            var currentStatus = await _statusRepository.GetById(existingRecord.StatusId);
            return currentStatus?.Name != "Inactive";
        }
    }
}
