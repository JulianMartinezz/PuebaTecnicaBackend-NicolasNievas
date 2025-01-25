using Challenge.DTO;

namespace Challenge.Service
{
    public interface IMedicalRecordService
    {
        Task<BaseResponse<TMedicalRecordDTO>> AddMedicalRecord(TMedicalRecordDTO request);
        Task<BaseResponse<TMedicalRecordDTO>> GetMedicalRecordById(int medicalRecordId);
    }
}
