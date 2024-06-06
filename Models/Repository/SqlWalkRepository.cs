using Bhandari.API.Data;
using Bhandari.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bhandari.API.Models.Repository
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly BhandariDbContext dbContext;

        public SqlWalkRepository(BhandariDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;

        }

        public async Task<Walk?> DeleteByIdAsync(Guid id)
        {
            var walkDomainModel = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x=>x.Id == id);
            if(walkDomainModel == null)
            {
                return null;
            }

            dbContext.Remove(walkDomainModel);
            await dbContext.SaveChangesAsync();
            return walkDomainModel;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn=null,string? filterQuery =null
            ,string? sortBy=null, bool isAscending=true)
        {
            var listofWalkDomainModel = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //filtering
            if(!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    listofWalkDomainModel = listofWalkDomainModel.Where(x=> x.Name.Contains(filterQuery));
                }
            }

            //sorting

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    listofWalkDomainModel = isAscending ? listofWalkDomainModel.OrderBy(x => x.Name) : listofWalkDomainModel.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    listofWalkDomainModel = isAscending ? listofWalkDomainModel.OrderBy(x => x.LengthInKm) : listofWalkDomainModel.OrderByDescending(x => x.LengthInKm);
                }
            }
            return await listofWalkDomainModel.ToListAsync();

        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walkDomainModel = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if(walkDomainModel == null)
            {
                return null;
            }
            return walkDomainModel;
        }

        public async Task<Walk?> UpdateByIdAsync(Guid id, Walk walk)
        {
            var walkDomainModel = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);

            if(walkDomainModel == null)
            {
                return null;
            }

            walkDomainModel.Name = walk.Name;
            walkDomainModel.Difficulty = walk.Difficulty;
            walkDomainModel.Description = walk.Description;
            walkDomainModel.LengthInKm = walk.LengthInKm;
            walkDomainModel.WalkImageUrl = walk.WalkImageUrl;
            walkDomainModel.DifficultyId = walk.DifficultyId;
            walkDomainModel.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();

            return walkDomainModel;
        }
    }
}
