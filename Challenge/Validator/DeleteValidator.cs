using Challenge.DTO;
using Challenge.Repository;
using FluentValidation;

namespace Challenge.Validator
{
    /// <summary>
    /// Validador para la eliminación de registros médicos.
    /// </summary>
    public class DeleteValidator : AbstractValidator<DeleteMedicalRecordDTO>
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="DeleteValidator"/>.
        /// </summary>
        /// <param name="statusRepository">Repositorio para la gestión de estados.</param>
        /// <param name="medicalRecordRepository">Repositorio para la gestión de registros médicos.</param>
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

            RuleFor(x => x.MedicalRecordId)
                .MustAsync(Exist).WithMessage("Medical Record does not exist");
        }

        /// <summary>
        /// Verifica si el registro médico existe.
        /// </summary>
        /// <param name="id">ID del registro médico.</param>
        /// <param name="token">Token para cancelación de la operación.</param>
        /// <returns>True si el registro médico existe; de lo contrario, false.</returns>
        private async Task<bool> Exist(int id, CancellationToken token)
        {
            var exists = await _medicalRecordRepository.GetMedicalRecordById(id);
            return exists != null;
        }

        /// <summary>
        /// Verifica si el estado del registro médico es válido para eliminar.
        /// </summary>
        /// <param name="deleteDto">DTO con los datos de eliminación.</param>
        /// <param name="cancellationToken">Token para cancelación de la operación.</param>
        /// <returns>True si el estado es válido; de lo contrario, false.</returns>
        private async Task<bool> BeValidStatus(DeleteMedicalRecordDTO deleteDto, CancellationToken cancellationToken)
        {
            var medicalRecord = await _medicalRecordRepository.GetMedicalRecordById(deleteDto.MedicalRecordId);

            if (medicalRecord == null)
                return true;

            var status = await _statusRepository.GetById(medicalRecord.StatusId);
            if (status == null)
                return false;

            return !status.Name!.Equals("Inactive");
        }
    }
}
