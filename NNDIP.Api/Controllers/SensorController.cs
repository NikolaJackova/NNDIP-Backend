using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Dtos.Sensor;
using NNDIP.Api.Dtos.YearPeriod;
using NNDIP.Api.Entities;
using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace NNDIP.Api.Controllers
{
    [Authorize]
    [Route("api/sensor")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public SensorController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(OperationId = "GetSensors")]
        public async Task<ActionResult<IEnumerable<SensorDto>>> GetSensors()
        {
            IEnumerable<Sensor> sensors = await _repositoryWrapper.SensorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SensorDto>>(sensors).ToList();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetSensor")]
        public async Task<ActionResult<SensorDto>> GetSensor(long id)
        {
            Sensor sensor = await _repositoryWrapper.SensorRepository.GetByIdAsync(id);

            if (sensor == null)
            {
                return NotFound();
            }

            return _mapper.Map<SensorDto>(sensor);
        }
    }
}
