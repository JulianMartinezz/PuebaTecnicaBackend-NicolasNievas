using Challenge.Models;

namespace Challenge.Repository
{
    public interface IMedicalRecordTypeRepository
    {
        Task<MedicalRecordType?> GetById(int? medicalRecordTypeId);
    }
}
