using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Dtos.Plan.LimitPlan;
using NNDIP.Api.Dtos.Sensor;
using NNDIP.Api.Dtos.YearPeriod;
using NNDIP.Api.Entities;
using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace NNDIP.Api.Controllers
{
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
