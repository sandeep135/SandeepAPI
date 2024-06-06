using Bhandari.API.Models.Domain;
using System.ComponentModel;

namespace Bhandari.API.Models.Repository
{
    public interface IRegionRepository
    {
        
        Task<List<Region>> GetAllASync();
        Task<Region?> GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id, Region region);

        Task<Region?> DeleteAsync(Guid id);
    }
}
