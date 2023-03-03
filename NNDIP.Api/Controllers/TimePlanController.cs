using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NNDIP.Api.Dtos.Plan.TimePlan;
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
        public async Task<ActionResult<IEnumerable<TimePlanDto>>> GetLimitPlans()
        {
            IEnumerable<LimitPlan> limitPlans = await _repositoryWrapper.LimitPlanRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LimitPlanDto>>(limitPlans).ToList();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetLimitPlan")]
        public async Task<ActionResult<LimitPlanDto>> GetLimitPlan(long id)
        {
            LimitPlan limitPlan = await _repositoryWrapper.LimitPlanRepository.GetByIdAsync(id);

            if (limitPlan == null)
            {
                return NotFound();
            }

            return _mapper.Map<LimitPlanDto>(limitPlan);
        }

        [HttpGet("settings")]
        [SwaggerOperation(OperationId = "GetLimitPlanSettings")]
        public async Task<ActionResult<LimitPlanSettings>> GetLimitPlanSettings()
        {
            return await _repositoryWrapper.LimitPlanRepository.GetLimitPlanSettingsAsync();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(OperationId = "PutLimitPlan")]
        public async Task<IActionResult> PutLimitPlan(long id, UpdateLimitPlanDto updateLimitPlanDto)
        {
            if (id != updateLimitPlanDto.Id)
            {
                return BadRequest();
            }
            LimitPlan limitPlan = _repositoryWrapper.LimitPlanRepository.GetById(id);
            _repositoryWrapper.LimitPlanRepository.Update(_mapper.Map(updateLimitPlanDto, limitPlan));

            try
            {
                await _repositoryWrapper.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repositoryWrapper.LimitPlanRepository.LimitPlanExists(id))
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
    }
}
