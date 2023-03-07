using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Dtos.Plan.ManualPlan;
using NNDIP.Api.Dtos.Plan.TimePlan;
using NNDIP.Api.Entities;
using NNDIP.Api.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace NNDIP.Api.Controllers
{
    [Authorize]
    [Route("api/manual-plan")]
    [ApiController]
    public class ManualPlanController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public ManualPlanController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(OperationId = "GetManualPlans")]
        public async Task<ActionResult<IEnumerable<ManualPlanDto>>> GetManualPlans()
        {
            IEnumerable<ManualPlan> manualPlans = await _repositoryWrapper.ManualPlanRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ManualPlanDto>>(manualPlans).ToList();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetManualPlan")]
        public async Task<ActionResult<ManualPlanDto>> GetManualPlan(long id)
        {
            ManualPlan manualPlan = await _repositoryWrapper.ManualPlanRepository.GetByIdAsync(id);

            if (manualPlan == null)
            {
                return NotFound();
            }

            return _mapper.Map<ManualPlanDto>(manualPlan);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(OperationId = "PutManualPlan")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutTimePlan(long id, UpdateManualPlanDto updateManualPlanDto)
        {
            if (id != updateManualPlanDto.Id)
            {
                return BadRequest();
            }
            ManualPlan manualPlan = _repositoryWrapper.ManualPlanRepository.GetById(id);
            _repositoryWrapper.ManualPlanRepository.Update(_mapper.Map(updateManualPlanDto, manualPlan));

            try
            {
                await _repositoryWrapper.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repositoryWrapper.ManualPlanRepository.ManualPlanExists(id))
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
        [SwaggerOperation(OperationId = "PostManualPlan")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TimePlanDto>> PostManualPlan(AddManualPlanDto addManualPlanDto)
        {
            _repositoryWrapper.ManualPlanRepository.AddAsync(_mapper.Map<ManualPlan>(addManualPlanDto));
            await _repositoryWrapper.SaveAsync();

            return CreatedAtAction("PostManualPlan", new { id = addManualPlanDto.Id }, addManualPlanDto);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(OperationId = "DeleteManualPlan")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteManualPlan(long id)
        {
            ManualPlan manualPlan = await _repositoryWrapper.ManualPlanRepository.GetByIdAsync(id);
            if (manualPlan == null)
            {
                return NotFound();
            }

            _repositoryWrapper.ManualPlanRepository.Remove(manualPlan);
            await _repositoryWrapper.SaveAsync();

            return NoContent();
        }
    }
}
