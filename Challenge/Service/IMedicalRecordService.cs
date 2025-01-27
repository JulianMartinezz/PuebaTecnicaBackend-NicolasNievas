using Challenge.DTO;

namespace Challenge.Service
{
    public interface IMedicalRecordService
    {
        Task<BaseResponse<TMedicalRecordDTO>> AddMedicalRecord(TMedicalRecordDTO request);
        Task<BaseResponse<IEnumerable<TMedicalRecordDTO>>> GetFilterMedicalRecords(MedicalRecordFilterDTO filter);
        Task<BaseResponse<TMedicalRecordDTO>> GetMedicalRecordById(int medicalRecordId);
        Task<BaseResponse<TMedicalRecordDTO>> DeleteMedicalRecord(DeleteMedicalRecordDTO deleteDto);
        Task<BaseResponse<TMedicalRecordDTO>> UpdateMedicalRecord(TMedicalRecordDTO dto);
    }
}
