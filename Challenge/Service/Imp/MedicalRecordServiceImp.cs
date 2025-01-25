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
        private readonly MedicalRecordValidator _medicalRecordValidator;

        public MedicalRecordServiceImp(IMedicalRecordRepository medicalRecordRepository, IMapper mapper, MedicalRecordValidator medicalRecordValidator)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
            _medicalRecordValidator = medicalRecordValidator;
        }

        public async Task<BaseResponse<TMedicalRecordDTO>> AddMedicalRecord(TMedicalRecordDTO request)
        {
            try
            {
                var medicalRecord = _mapper.Map<TMedicalRecord>(request);

                medicalRecord.CreationDate = DateOnly.FromDateTime(DateTime.Now);
                medicalRecord.StatusId = 1;
                medicalRecord.ModificationDate = DateOnly.FromDateTime(DateTime.Today);

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
                    Exception = ex.Message
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
                    Exception = ex.Message
                };
            }
        }
    }
}
