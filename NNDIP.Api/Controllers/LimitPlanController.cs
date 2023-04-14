using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Dtos.Plan.LimitPlan;
using NNDIP.Api.Dtos.YearPeriod;
using NNDIP.Api.Entities;
using NNDIP.Api.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace NNDIP.Api.Controllers
{
    [Authorize]
    [Route("api/limit-plan")]
    [ApiController]
    public class LimitPlanController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public LimitPlanController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(OperationId = "GetLimitPlans")]
        public async Task<ActionResult<IEnumerable<LimitPlanDto>>> GetLimitPlans()
        {
            IEnumerable<LimitPlan> limitPlans = await _repositoryWrapper.LimitPlanRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LimitPlanDto>>(limitPlans).ToList();
        }

        [HttpGet("enabled")]
        [SwaggerOperation(OperationId = "GetEnabledLimitPlans")]
        public async Task<ActionResult<IEnumerable<LimitPlanDto>>> GetEnabledLimitPlans()
        {
            IEnumerable<LimitPlan> limitPlans = await _repositoryWrapper.LimitPlanRepository.GetEnabledLimitPlansAsync();
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

        [HttpPut("settings")]
        [SwaggerOperation(OperationId = "PutLimitPlanSettings")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult?> PutLimitPlanSettings(LimitPlanSettings limitPlanSettings)
        {
            YearPeriod yearPeriod = _repositoryWrapper.YearPeriodRepository.GetById(limitPlanSettings.YearPeriodDto.Id);
            _repositoryWrapper.YearPeriodRepository.Update(_mapper.Map(_mapper.Map<UpdateYearPeriodDto>(limitPlanSettings.YearPeriodDto), yearPeriod));

            _repositoryWrapper.LimitPlanRepository.Update(GetModifiedLimitPlan(limitPlanSettings.TemperatureLow, limitPlanSettings.OptimalValueTemperature));
            _repositoryWrapper.LimitPlanRepository.Update(GetModifiedLimitPlan(limitPlanSettings.TemperatureHigh, limitPlanSettings.OptimalValueTemperature));
            _repositoryWrapper.LimitPlanRepository.Update(GetModifiedLimitPlan(limitPlanSettings.Co2, limitPlanSettings.OptimalValueCo2));

            _repositoryWrapper.PlanRepository.Update(GetModifiedPlan(limitPlanSettings.TemperatureLow));
            _repositoryWrapper.PlanRepository.Update(GetModifiedPlan(limitPlanSettings.TemperatureHigh));
            _repositoryWrapper.PlanRepository.Update(GetModifiedPlan(limitPlanSettings.Co2));
            try
            {
                await _repositoryWrapper.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        private LimitPlan GetModifiedLimitPlan(Threshold threshold, double optimalValue)
        {
            LimitPlan limitPlan = _repositoryWrapper.LimitPlanRepository.GetById(threshold.Id);
            limitPlan.OptimalValue = optimalValue;
            limitPlan.ThresholdValue = threshold.Value;
            return limitPlan;
        }

        private Plan GetModifiedPlan(Threshold threshold)
        {
            Plan plan = _repositoryWrapper.PlanRepository.GetById(threshold.Id);
            plan.Enabled = threshold.Enabled;
            plan.EventId = threshold.EventId;
            return plan;
        }
    }
}
