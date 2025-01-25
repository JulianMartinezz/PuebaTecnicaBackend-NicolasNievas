using Challenge.Data;
using Challenge.Models;

namespace Challenge.Repository.imp
{
    public class StatusRepositoryImp : IStatusRepository
    {
        private readonly MedicalContext _context;

        public StatusRepositoryImp(MedicalContext context)
        {
            _context = context;
        }
        public async Task<Status?> GetById(int? statusId)
        {
            return await _context.Statuses.FindAsync(statusId);
        }
    }
}
