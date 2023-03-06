using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Dtos.Plan.TimePlan;
using NNDIP.Api.Entities;
using NNDIP.Api.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace NNDIP.Api.Controllers
{
    [Authorize]
    [Route("api/time-plan")]
    [ApiController]
    public class TimePlanController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public TimePlanController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(OperationId = "GetTimePlans")]
        public async Task<ActionResult<IEnumerable<TimePlanDto>>> GetTimePlans()
        {
            IEnumerable<TimePlan> timePlans = await _repositoryWrapper.TimePlanRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TimePlanDto>>(timePlans).ToList();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetTimePlan")]
        public async Task<ActionResult<TimePlanDto>> GetTimePlan(long id)
        {
            TimePlan timePlan = await _repositoryWrapper.TimePlanRepository.GetByIdAsync(id);

            if (timePlan == null)
            {
                return NotFound();
            }

            return _mapper.Map<TimePlanDto>(timePlan);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(OperationId = "PutTimePlan")]
        public async Task<IActionResult> PutTimePlan(long id, UpdateTimePlanDto updateTimePlanDto)
        {
            if (id != updateTimePlanDto.Id)
            {
                return BadRequest();
            }
            TimePlan timePlan = _repositoryWrapper.TimePlanRepository.GetById(id);
            _repositoryWrapper.TimePlanRepository.Update(_mapper.Map(updateTimePlanDto, timePlan));

            try
            {
                await _repositoryWrapper.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repositoryWrapper.TimePlanRepository.TimePlanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(OperationId = "PostTimePlan")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TimePlanDto>> PostTimePlan(AddTimePlanDto addTimePlanDto)
        {
            _repositoryWrapper.TimePlanRepository.AddAsync(_mapper.Map<TimePlan>(addTimePlanDto));
            await _repositoryWrapper.SaveAsync();

            return CreatedAtAction("PostTimePlan", new { id = addTimePlanDto.Id }, addTimePlanDto);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(OperationId = "DeleteTimePlan")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTimePlan(long id)
        {
            TimePlan timePlan = await _repositoryWrapper.TimePlanRepository.GetByIdAsync(id);
            if (timePlan == null)
            {
                return NotFound();
            }

            _repositoryWrapper.TimePlanRepository.Remove(timePlan);
            await _repositoryWrapper.SaveAsync();

            return NoContent();
        }
    }
}
