using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NNDIP.Api.Dtos.Data;
using NNDIP.Api.Dtos.Sensor;
using NNDIP.Api.Dtos.YearPeriod;
using NNDIP.Api.Entities;
using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace NNDIP.Api.Controllers
{
    [Authorize]
    [Route("api/data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public DataController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(OperationId = "GetDatas")]
        public async Task<ActionResult<IEnumerable<DataDto>>> GetData([FromQuery(Name = "numberOfResults")] int? numberOfResults = null)
        {
            IEnumerable<Data> data = await _repositoryWrapper.DataRepository.GetAllDescAsync(numberOfResults);
            return _mapper.Map<IEnumerable<DataDto>>(data).ToList();
        }
        
        [HttpGet("actual")]
        [SwaggerOperation(OperationId = "GetActualData")]
        public async Task<ActionResult<IEnumerable<DataDto>>> GetActualData()
        {
            IEnumerable<Data> data = await _repositoryWrapper.DataRepository.GetActualDataAsync();
            return _mapper.Map<IEnumerable<DataDto>>(data).ToList();
        }

        [HttpGet("actual/{sensorId}")]
        [SwaggerOperation(OperationId = "GetActualSensorData")]
        public async Task<ActionResult<IEnumerable<DataDto>>> GetActualSensorData(long sensorId)
        {
            IEnumerable<Data> data = await _repositoryWrapper.DataRepository.GetActualSensorDataAsync(sensorId);
            return _mapper.Map<IEnumerable<DataDto>>(data).ToList();
        }

        [HttpGet("historical")]
        [SwaggerOperation(OperationId = "GetHistoricalData")]
        public async Task<ActionResult<IEnumerable<DataDto>>> GetHistoricalData([FromQuery(Name = "from")] DateTime? dateFrom = null, [FromQuery(Name = "to")] DateTime? dateTo = null)
        {
            IEnumerable<Data> data = await _repositoryWrapper.DataRepository.GetHistoricalDataAsync(dateFrom, dateTo);
            return _mapper.Map<IEnumerable<DataDto>>(data).ToList();
        }

        [HttpGet("historical/{sensorId}")]
        [SwaggerOperation(OperationId = "GetHistoricalSensorData")]
        public async Task<ActionResult<IEnumerable<DataDto>>> GetHistoricalSensorData(long sensorId, [FromQuery(Name = "from")] DateTime? dateFrom = null, [FromQuery(Name = "to")] DateTime? dateTo = null)
        {
            IEnumerable<Data> data = await _repositoryWrapper.DataRepository.GetHistoricalSensorDataAsync(sensorId, dateFrom, dateTo);
            return _mapper.Map<IEnumerable<DataDto>>(data).ToList();
        }

        [HttpGet("{dataId}")]
        [SwaggerOperation(OperationId = "GetData")]
        public async Task<ActionResult<DataDto>> GetData(long dataId)
        {
            var data = await _repositoryWrapper.DataRepository.GetByIdAsync(dataId);

            if (data == null)
            {
                return NotFound();
            }

            return _mapper.Map<DataDto>(data);
        }
    }
}
