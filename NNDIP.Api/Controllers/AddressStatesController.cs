using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NNDIP.Api.Dtos.AddressState;
using NNDIP.Api.Entities;
using NNDIP.Api.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace NNDIP.Api.Controllers
{
    [Authorize]
    [Route("api/address-state")]
    [ApiController]
    public class AddressStatesController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public AddressStatesController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(OperationId = "GetAddressStates")]
        public async Task<ActionResult<IEnumerable<AddressStateDto>>> GetAddressStates()
        {
            IEnumerable<AddressState> addressStates = await _repositoryWrapper.AddressStateRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AddressStateDto>>(addressStates).ToList();
        }

        [HttpGet("results")]
        [SwaggerOperation(OperationId = "GetAddressResults")]
        public async Task<ActionResult<AddressStateResultDto>> GetAddressResults()
        {
            return await _repositoryWrapper.AddressStateRepository.GetAddressStateResultAsync();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetAddressState")]
        public async Task<ActionResult<AddressStateDto>> GetAddressState(long id)
        {
            var addressState = await _repositoryWrapper.AddressStateRepository.GetByIdAsync(id);

            if (addressState == null)
            {
                return NotFound();
            }

            return _mapper.Map<AddressStateDto>(addressState);
        }
    }
}
