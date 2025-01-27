using Challenge.Repository;
using FluentValidation;

namespace Challenge.Validator
{
    /// <summary>
    /// Validador para la creación de registros médicos.
    /// </summary>
    public class CreateValidator : MedicalRecordValidator
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="CreateValidator"/>.
        /// </summary>
        /// <param name="statusRepository">Repositorio para la gestión de estados.</param>
        /// <param name="medicalRecordTypeRepository">Repositorio para la gestión de tipos de registros médicos.</param>
        public CreateValidator(IStatusRepository statusRepository, IMedicalRecordTypeRepository medicalRecordTypeRepository)
            : base(statusRepository, medicalRecordTypeRepository)
        {
            RuleFor(x => x.StatusId)
            .Must(statusId => statusId == 1)
            .WithMessage("Cannot assign Inactive status when creating a new record");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created By is required");

            RuleFor(x => x.EndDate)
                .Null().WithMessage("End Date must not be provided when creating a new record");
        }
    }
}
