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

        [HttpGet("data")]
        [SwaggerOperation(OperationId = "GetActualSensorsData")]
        public async Task<ActionResult<IEnumerable<SensorsDataDto>>> GetActualSensorsData()
        {
            IEnumerable<Data> data = await _repositoryWrapper.DataRepository.GetActualDataAsync();
            List<SensorsDataDto> sensorsDataDtos = new List<SensorsDataDto>();
            foreach (var dataItem in data)
            {
                SensorsDataDto sensorsDataDto = new SensorsDataDto()
                {
                    Id = dataItem.Sensor.Id,
                    Name = dataItem.Sensor.Name,
                    SensorType = dataItem.Sensor.SensorType,
                    DataTimestamp = dataItem.DataTimestamp
                };

                //refactor?? 
                if (dataItem.Co2 is not null)
                {
                    sensorsDataDto.Data.Add(new Dtos.Data.SimpleDataDto()
                    {
                        Type = Enums.SensorDataType.CO2,
                        Value = dataItem.Co2
                    });
                }
                if (dataItem.Humidity is not null)
                {
                    sensorsDataDto.Data.Add(new Dtos.Data.SimpleDataDto()
                    {
                        Type = Enums.SensorDataType.HUMIDITY,
                        Value = dataItem.Humidity
                    });
                }
                if (dataItem.Temperature is not null)
                {
                    sensorsDataDto.Data.Add(new Dtos.Data.SimpleDataDto()
                    {
                        Type = Enums.SensorDataType.TEMPERATURE,
                        Value = dataItem.Temperature
                    });
                }
                sensorsDataDtos.Add(sensorsDataDto);
            } 
            return sensorsDataDtos;
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
