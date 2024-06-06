using Bhandari.API.Models.Domain;
using Bhandari.API.Models.DTOs;

namespace Bhandari.API.Models.Repository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn=null,string? filterQuery=null,string? sortBy=null, bool isAscending=true);

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?> UpdateByIdAsync(Guid id, Walk walk);

        Task<Walk?> DeleteByIdAsync(Guid id);
    }
}
