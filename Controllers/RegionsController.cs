using AutoMapper;
using Bhandari.API.CustomActionFilters;
using Bhandari.API.Data;
using Bhandari.API.Models.Domain;
using Bhandari.API.Models.DTOs;
using Bhandari.API.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bhandari.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        private readonly BhandariDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(BhandariDbContext dbContext, IRegionRepository regionRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        // https://localhost.portnumber/api/regions
        // GET ALL REGIONS
        // GET:https://localhost.portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // get dbcontext as domain model
            var regionDomains = await regionRepository.GetAllASync();

            // map it to DTos

            //var regionsDtos = new List<RegionDto>();

            //foreach(var regionDomain in regionDomains)
            //{
            //    regionsDtos.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl
            //    }

            //    );
            //}
            var regionsDtos = mapper.Map<List<RegionDto>>(regionDomains);
            //return Dtos
            return Ok(regionsDtos);
            
        }

        // GET: https://localhost.portnumber/api/regions/{id}
        // Get Region by ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            // get regionDomain from DBcontext
            var regionDomains = await regionRepository.GetByIdAsync(id);

            if (regionDomains == null)
            {
                return NotFound();
            }

            //map it to Dtos
            //var regionsDtos = new RegionDto()
            //{
            //    Id = regionDomains.Id,
            //    Code = regionDomains.Code,
            //    Name = regionDomains.Name,
            //    RegionImageUrl = regionDomains.RegionImageUrl
            //};
            var regionsDtos = mapper.Map<RegionDto>(regionDomains);

            return Ok(regionsDtos);
        }

        // POST: Create region
        //POST: https://localhost.portnumber/api/regions
        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody]AddRegionRequestDtocs addRegionRequestDtocs)
        {
            //convert DTO to dbcontext domain model

            //var regionDomainModel = new Region()
            //{
            //    Code = addRegionRequestDtocs.Code,
            //    Name = addRegionRequestDtocs.Name,
            //    RegionImageUrl = addRegionRequestDtocs.RegionImageUrl
            //};
            
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDtocs);

                await regionRepository.CreateAsync(regionDomainModel);

                //var regionsDtos = new RegionDto()
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl
                //};

                var regionsDtos = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionsDtos.Id }, regionsDtos);
            
            
        }

        //UPDATE: to update region
        //UPDATE : https://localhost.portnumber/api/regions/{id}
        [ValidateModel]
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //check if the id exits
            //var regionDomainModel = new Region()
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //};            

          
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                //convert back to dto model to return
                //var regionDtos = new RegionDto()
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl
                //};

                var regionDtos = mapper.Map<RegionDto>(regionDomainModel);

                return Ok(regionDtos);
            

           
            
        }

        //DELETE: delete a region
        // DELETE : https://localhost.portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound(); 
            }                       

            //var regionDtos = new RegionDto()
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDtos = mapper.Map<RegionDto> (regionDomainModel);
            return Ok(regionDtos);
        }
    }
}
