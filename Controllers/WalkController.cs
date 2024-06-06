using AutoMapper;
using Bhandari.API.CustomActionFilters;
using Bhandari.API.Models.Domain;
using Bhandari.API.Models.DTOs;
using Bhandari.API.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bhandari.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalkController(IMapper mapper,IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        //Post a walk
        // POST: /api/walk
        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
           
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);

                var walkDto = mapper.Map<WalkDto>(walkDomainModel);

                return Ok(walkDto);
             
            
        }

        //Get list of walks
        //GET: /api/walk?Name=filterOn&filterQuery=Track&sortBy=Length&isAscending
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn = null, [FromQuery] string? filterQuery=null,
            [FromQuery] string? sortBy=null, [FromQuery] bool? isAscending=true)
        {
            var walkDomainModel = await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending??true);
            if(walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(walkDomainModel);
        }
        
        //GET Walk by Id
        // GET: /api/Walk/{id}
        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if(walkDomainModel == null)
            {
                return NotFound();
            }
            var walkDTo = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDTo);
        }

        //UPDATE Walk by Id
        // PUT : Update Walk By Id
        [ValidateModel]
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

                walkDomainModel = await walkRepository.UpdateByIdAsync(id, walkDomainModel);
                if (walkDomainModel == null)
                {
                    return NotFound();
                }
                var walkDto = mapper.Map<WalkDto>(walkDomainModel);
                return Ok(walkDto);
            
           

        }

        //DELETE Walk by Id
        //DELETE: api/walk/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }
    }

    
}
