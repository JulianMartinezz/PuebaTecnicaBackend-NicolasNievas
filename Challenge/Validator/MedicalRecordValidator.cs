﻿using Challenge.DTO;
using Challenge.Models;
using Challenge.Repository;
using FluentValidation;

namespace Challenge.Validator
{
    /// <summary>
    /// Validador base para las entidades de registros médicos.
    /// Este validador asegura que los datos del DTO cumplan con las reglas de negocio y validaciones necesarias.
    /// </summary>
    public class MedicalRecordValidator : AbstractValidator<TMedicalRecordDTO>
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMedicalRecordTypeRepository _medicalRecordTypeRepository;

        /// <summary>
        /// Constructor de la clase <see cref="MedicalRecordValidator"/>.
        /// </summary>
        /// <param name="statusRepository">Repositorio para obtener información sobre los estados.</param>
        /// <param name="medicalRecordTypeRepository">Repositorio para obtener información sobre los tipos de registros médicos.</param>
        public MedicalRecordValidator(IStatusRepository statusRepository, IMedicalRecordTypeRepository medicalRecordTypeRepository)
        {
            _statusRepository = statusRepository;
            _medicalRecordTypeRepository = medicalRecordTypeRepository;

            // 2.2 Required Fields Validation

            RuleFor(x => x.Diagnosis)
                .NotEmpty().WithMessage("Diagnosis is required")
                .MaximumLength(100).WithMessage("Diagnosis must not exceed 100 characters");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start Date is required")
                //.LessThan(x => x.EndDate).WithMessage("Start Date must be less than End Date")
                .Must(BeNotFutureDate).WithMessage("Start Date cannot be a future date");

            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage("Status is required")
                .MustAsync((statusID, cancellationToken) => BeValidStatus(statusID, cancellationToken))
                .WithMessage("Invalid Status");

            RuleFor(x => x.MedicalRecordTypeId)
                .NotEmpty().WithMessage("Medical Record Type is required")
                .MustAsync((medicalRecordTypeId, cancellationToken) => BeValidMedicalRecordType(medicalRecordTypeId, cancellationToken))
                .WithMessage("Invalid Medical Record Type");

            RuleFor(x => x.FileId).NotEmpty().WithMessage("File is required");


            // 2.4 Maximum Length Validation

            RuleFor(x => x.MotherData)
                .MaximumLength(2000).WithMessage("Mother Data must not exceed 2000 characters");

            RuleFor(x => x.FatherData)
                .MaximumLength(2000).WithMessage("Father Data must not exceed 2000 characters");

            RuleFor(x => x.OtherFamilyData)
                .MaximumLength(2000).WithMessage("Other Family Data must not exceed 2000 characters");

            RuleFor(x => x.MedicalBoard)
                .MaximumLength(200).WithMessage("Medical Board must not exceed 200 characters");

            RuleFor(x => x.DeletionReason)
                .MaximumLength(2000).WithMessage("Deletion Reason must not exceed 2000 characters");

            RuleFor(x => x.Observations)
                .MaximumLength(2000).WithMessage("Observations must not exceed 2000 characters");

            string[] yesNoFields = new[] {
            "Audiometry", "PositionChange", "ExecuteMicros",
            "ExecuteExtra", "VoiceEvaluation", "Disability", "AreaChange"
            };

            foreach (var field in yesNoFields)
            {
                RuleFor(x => x.GetType().GetProperty(field).GetValue(x))
                    .Must(value => value == null || value.ToString().ToUpper() == "Y" || value.ToString().ToUpper() == "N")
                    .WithMessage($"{field} must be 'Y' or 'N'");
            }

            When(x => x.Disability?.ToUpper() == "Y", () =>
            {
                RuleFor(x => x.DisabilityPercentage)
                    .NotNull().WithMessage("Disability Percentage is required when Disability is Yes")
                    .InclusiveBetween(0, 100).WithMessage("Disability Percentage must be between 0 and 100");
            });

            When(x => x.PositionChange?.ToUpper() == "Y", () =>
            {
                RuleFor(x => x.Observations)
                    .NotEmpty().WithMessage("Observations are required when Position Change is Yes");
            });

            When(x => x.EndDate.HasValue, () => {
                RuleFor(x => x.StartDate)
                    .Must((m, startDate) => startDate < m.EndDate)
                    .WithMessage("Start Date must be less than End Date");
            });
        }

        /// <summary>
        /// Valida que la fecha de inicio no sea una fecha futura.
        /// </summary>
        /// <param name="startDate">Fecha de incio</param>
        /// <returns>True si la fecha no es futura; de lo contrario, false.</returns>
        private bool BeNotFutureDate(DateOnly? startDate){
            return startDate <= DateOnly.FromDateTime(DateTime.Today);
        }

        /// <summary>
        /// Valida si el tipo de registro médico es válido.
        /// </summary>
        /// <param name="medicalRecordTypeId">ID del tipo de registro médico.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>True si el tipo es válido; de lo contrario, false.</returns>
        private async Task<bool> BeValidMedicalRecordType(int? medicalRecordTypeId, CancellationToken cancellationToken){
            var medicalRecordType = await _medicalRecordTypeRepository.GetById(medicalRecordTypeId);
            return medicalRecordType != null;
        }

        /// <summary>
        /// Valida si el estado proporcionado es válido.
        /// </summary>
        /// <param name="statusId">ID del estado.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>True si el estado es válido; de lo contrario, false.</returns>
        private async Task<bool> BeValidStatus(int? statusId, CancellationToken cancellationToken)
        {
            var status = await _statusRepository.GetById(statusId);
            return status != null;
        }
    }
}
