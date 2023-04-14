using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Entities;
using NNDIP.Api.Dtos.YearPeriod;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using NNDIP.Api.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace NNDIP.Api.Controllers
{
    [Authorize]
    [Route("api/year-period")]
    [ApiController]
    public class YearPeriodController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        public YearPeriodController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(OperationId = "GetYearPeriods")]
        public async Task<ActionResult<IEnumerable<SimpleYearPeriodDto>>> GetYearPeriods()
        {
            IEnumerable<YearPeriod> yearPeriods = await _repositoryWrapper.YearPeriodRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SimpleYearPeriodDto>>(yearPeriods).ToList();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetYearPeriod")]
        public async Task<ActionResult<YearPeriodDto>> GetYearPeriod(long id)
        {
            YearPeriod? yearPeriod = await _repositoryWrapper.YearPeriodRepository.GetByIdAsync(id);

            if (yearPeriod == null)
            {
                return NotFound();
            }

            return _mapper.Map<YearPeriodDto>(yearPeriod);
        }

        [HttpGet("active")]
        [SwaggerOperation(OperationId = "GetActiveYearPeriod")]
        public async Task<ActionResult<SimpleYearPeriodDto>> GetActiveYearPeriod()
        {
            YearPeriod yearPeriod = await _repositoryWrapper.YearPeriodRepository.GetActiveYearPeriodAsync();

            if (yearPeriod == null)
            {
                return NotFound();
            }

            return _mapper.Map<SimpleYearPeriodDto>(yearPeriod);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(OperationId = "PutYearPeriod")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutYearPeriod(long id, UpdateYearPeriodDto updateYearPeriodDto)
        {
            if (id != updateYearPeriodDto.Id)
            {
                return BadRequest();
            }
            YearPeriod yearPeriod = _repositoryWrapper.YearPeriodRepository.GetById(id);
            _repositoryWrapper.YearPeriodRepository.Update(_mapper.Map(updateYearPeriodDto, yearPeriod));
            try
            {
                await _repositoryWrapper.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repositoryWrapper.YearPeriodRepository.YearPeriodExists(id))
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
