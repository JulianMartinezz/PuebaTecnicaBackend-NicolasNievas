using Challenge.DTO;
using Challenge.Repository;
using FluentValidation;

namespace Challenge.Validator
{
    /// <summary>
    /// Validador para la actualización de registros médicos.
    /// </summary>
    public class UpdateValidator : MedicalRecordValidator
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IStatusRepository _statusRepository;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="UpdateValidator"/>.
        /// </summary>
        /// <param name="statusRepository">Repositorio para la gestión de estados.</param>
        /// <param name="medicalRecordTypeRepository">Repositorio para la gestión de tipos de registros médicos.</param>
        /// <param name="medicalRecordRepository">Repositorio para la gestión de registros médicos.</param>
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

                RuleFor(x => x.DeletedBy)
                    .NotEmpty().WithMessage("Deleted By is required when changing to Inactive status");
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

        /// <summary>
        /// Valida que el registro médico pueda ser actualizado.
        /// </summary>
        /// <param name="dto">Objeto <see cref="TMedicalRecordDTO"/> a validar.</param>
        /// <param name="token">Token para cancelación de la operación.</param>
        /// <returns>True si el registro es válido para actualizar; de lo contrario, false.</returns>
        private async Task<bool> BeValidForUpdate(TMedicalRecordDTO dto, CancellationToken token)
        {
            var existingRecord = await _medicalRecordRepository.GetMedicalRecordById(dto.MedicalRecordId);
            if (existingRecord == null) return false;

            var currentStatus = await _statusRepository.GetById(existingRecord.StatusId);
            return currentStatus?.Name != "Inactive";
        }
    }
}
