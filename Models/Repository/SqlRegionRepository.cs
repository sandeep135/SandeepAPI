using Bhandari.API.Data;
using Bhandari.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bhandari.API.Models.Repository
{
    public class SqlRegionRepository: IRegionRepository
    {
        private readonly BhandariDbContext dbContext;
        
        public SqlRegionRepository(BhandariDbContext dbContext)
        {
            this.dbContext = dbContext;            
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = dbContext.Regions.FirstOrDefault(s => s.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            dbContext.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllASync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
            if(regionDomain == null)
            {
                return null;
            }
            regionDomain.Name = region.Name;
            regionDomain.Code = region.Code;
            regionDomain.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return regionDomain;
            
        }
    }
}
