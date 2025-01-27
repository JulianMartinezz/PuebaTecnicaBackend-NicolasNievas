using Challenge.Data;
using Challenge.Models;

namespace Challenge.Repository.imp
{
    public class MedicalRecordTypeRepositoryImp : IMedicalRecordTypeRepository
    {
        private readonly MedicalContext _context;
        public MedicalRecordTypeRepositoryImp(MedicalContext medicalContext) {
            _context = medicalContext;
        }
        public async Task<MedicalRecordType?> GetById(int? medicalRecordTypeId){
            return await _context.MedicalRecordTypes.FindAsync(medicalRecordTypeId);
        }
    }
}
