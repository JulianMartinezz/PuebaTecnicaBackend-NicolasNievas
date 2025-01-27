using AutoMapper;
using Challenge.DTO;
using Challenge.Models;
using Challenge.Repository;
using Challenge.Validator;

namespace Challenge.Service.Imp
{
    public class MedicalRecordServiceImp : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;
        private readonly IStatusRepository _statusRepository;
        private readonly CreateValidator _createValidator;
        private readonly UpdateValidator _updateValidator;

        public MedicalRecordServiceImp(IMedicalRecordRepository medicalRecordRepository, 
            IMapper mapper, 
            MedicalRecordValidator medicalRecordValidator, 
            IStatusRepository statusRepository,
            CreateValidator createValidator,
            UpdateValidator updateValidator)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
            _statusRepository = statusRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<BaseResponse<TMedicalRecordDTO>> AddMedicalRecord(TMedicalRecordDTO request)
        {
            try
            {
                var validationResult = await _createValidator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    return new BaseResponse<TMedicalRecordDTO>
                    {
                        Success = false,
                        Message = "Validation Failed",
                        Exception = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                        Code = 400
                    };
                }

                var medicalRecord = _mapper.Map<TMedicalRecord>(request);

                medicalRecord.CreationDate = DateOnly.FromDateTime(DateTime.Now);
                medicalRecord.StatusId = 1;
                medicalRecord.CreatedBy = request.CreatedBy;

                var addedRecord = await _medicalRecordRepository.AddMedicalRecord(medicalRecord);

                var addedRecordDto = _mapper.Map<TMedicalRecordDTO>(addedRecord);

                return new BaseResponse<TMedicalRecordDTO>
                {
                    Success = true,
                    Message = "Medical Record Added Successfully",
                    Data = addedRecordDto,
                    Code = 201
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TMedicalRecordDTO>
                {
                    Success = false,
                    Message = "Error Adding Medical Record",
                    Exception = ex.Message,
                    Code = 500
                };
            }
        }

        public async Task<BaseResponse<TMedicalRecordDTO>> DeleteMedicalRecord(DeleteMedicalRecordDTO deleteDto)
        {
            try
            {
                var validator = new DeleteValidator(_statusRepository, _medicalRecordRepository);
                var validationResult = await validator.ValidateAsync(deleteDto);

                if (!validationResult.IsValid)
                {
                    return new BaseResponse<TMedicalRecordDTO>
                    {
                        Success = false,
                        Message = "Validation Failed",
                        Exception = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                        Code = 400
                    };
                }

                var medicalRecord = await _medicalRecordRepository.GetMedicalRecordById(deleteDto.MedicalRecordId);

                medicalRecord.StatusId = 2;
                medicalRecord.DeletionDate = DateOnly.FromDateTime(DateTime.Today);
                medicalRecord.DeletedBy = deleteDto.DeletedBy;
                medicalRecord.DeletionReason = deleteDto.DeletionReason;
                medicalRecord.EndDate = DateOnly.FromDateTime(DateTime.Today);

                var updatedRecord = await _medicalRecordRepository.UpdateMedicalRecord(medicalRecord);

                var updatedRecordDto = _mapper.Map<TMedicalRecordDTO>(updatedRecord);

                return new BaseResponse<TMedicalRecordDTO>
                {
                    Success = true,
                    Message = "Medical Record Deleted Successfully",
                    Data = updatedRecordDto,
                    Code = 200
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TMedicalRecordDTO>
                {
                    Success = false,
                    Message = "Error Deleting Medical Record",
                    Exception = ex.Message,
                    Code = 500
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<TMedicalRecordDTO>>> GetFilterMedicalRecords(MedicalRecordFilterDTO filter)
        {
            try
            {
                if (filter.Page <= 0) filter.Page = 1;
                if (filter.PageSize <= 0) filter.PageSize = 10;

                var (medical, totalCount) = await _medicalRecordRepository.GetFilterMedicalRecords(filter);

                if (medical == null || !medical.Any())
                {
                    return new BaseResponse<IEnumerable<TMedicalRecordDTO>>
                    {
                        Success = true,
                        Message = "No medical records found",
                        Data = new List<TMedicalRecordDTO>(),
                        TotalRows = 0,
                        Code = 204
                    };
                }

                var medicalDtos = _mapper.Map<IEnumerable<TMedicalRecordDTO>>(medical);

                return new BaseResponse<IEnumerable<TMedicalRecordDTO>>
                {
                    Success = true,
                    Message = "Medical Records Retrieved Successfully",
                    Data = medicalDtos,
                    TotalRows = totalCount,
                    Code = 200
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TMedicalRecordDTO>>
                {
                    Success = false,
                    Message = "Error Getting Medical Records",
                    Exception = ex.Message,
                    Code = 500
                };
            }
        }

        public async Task<BaseResponse<TMedicalRecordDTO>> GetMedicalRecordById(int medicalRecordId)
        {
            try
            {
                var medicalRecord = await _medicalRecordRepository.GetMedicalRecordById(medicalRecordId);

                if (medicalRecord == null)
                {
                    return new BaseResponse<TMedicalRecordDTO>
                    {
                        Success = false,
                        Message = "Medical Record Not Found",
                        Code = 404
                    };
                }

                var medicalRecordDto = _mapper.Map<TMedicalRecordDTO>(medicalRecord);

                return new BaseResponse<TMedicalRecordDTO>
                {
                    Success = true,
                    Message = "Medical Record Found",
                    Data = medicalRecordDto,
                    Code = 200
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TMedicalRecordDTO>
                {
                    Success = false,
                    Message = "Error Getting Medical Record",
                    Exception = ex.Message,
                    Code = 500
                };
            }
        }

        public async Task<BaseResponse<TMedicalRecordDTO>> UpdateMedicalRecord(TMedicalRecordDTO dto)
        {
            try
            {
                var validationResult = await _updateValidator.ValidateAsync(dto);

                if (!validationResult.IsValid)
                {
                    return new BaseResponse<TMedicalRecordDTO>
                    {
                        Success = false,
                        Message = "Validation Failed",
                        Exception = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                        Code = 400
                    };
                }

                var medicalRecord = await _medicalRecordRepository.GetMedicalRecordById(dto.MedicalRecordId);

                if (medicalRecord == null)
                {
                    return new BaseResponse<TMedicalRecordDTO>
                    {
                        Success = false,
                        Message = "Medical Record Not Found",
                        Code = 404
                    };
                }

                _mapper.Map(dto, medicalRecord);

                medicalRecord.ModificationDate = DateOnly.FromDateTime(DateTime.Today);

                if (dto.StatusId == 2)
                {
                    medicalRecord.DeletionReason = dto.DeletionReason;
                    medicalRecord.DeletionDate = DateOnly.FromDateTime(DateTime.Today);
                    medicalRecord.DeletedBy = dto.ModifiedBy;
                }

                var updated = await _medicalRecordRepository.UpdateMedicalRecord(medicalRecord);
                var updatedDto = _mapper.Map<TMedicalRecordDTO>(updated);

                return new BaseResponse<TMedicalRecordDTO>
                {
                    Success = true,
                    Message = "Medical Record Updated Successfully",
                    Data = updatedDto,
                    Code = 200
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TMedicalRecordDTO>
                {
                    Success = false,
                    Message = "Error Updating Medical Record",
                    Exception = ex.Message,
                    Code = 500
                };
            }
        }
    }
}
