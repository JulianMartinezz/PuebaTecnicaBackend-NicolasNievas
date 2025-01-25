using Challenge.Models;

namespace Challenge.Repository
{
    public interface IStatusRepository
    {
        Task<Status?> GetById(int? statusId);
    }
}
