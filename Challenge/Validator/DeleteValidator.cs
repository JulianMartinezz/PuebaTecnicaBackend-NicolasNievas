using Challenge.DTO;
using Challenge.Repository;
using FluentValidation;

namespace Challenge.Validator
{
    public class DeleteValidator : AbstractValidator<DeleteMedicalRecordDTO>
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

            RuleFor(x => x)
                .MustAsync(BeValidStatus).WithMessage("Medical Record is already Inactive");
        }

        private async Task<bool> BeValidStatus(DeleteMedicalRecordDTO deleteDto, CancellationToken cancellationToken)
        {
            var medicalRecord = await _medicalRecordRepository.GetMedicalRecordById(deleteDto.MedicalRecordId);

            if (medicalRecord == null)
                return false;

            var status = await _statusRepository.GetById(medicalRecord.StatusId);
            if (status == null)
                return false;

            return !status.Name!.Equals("Inactive");
        }
    }
}
