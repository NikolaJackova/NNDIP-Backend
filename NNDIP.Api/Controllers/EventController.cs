using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NNDIP.Api.Dtos.Event;
using NNDIP.Api.Entities;
using NNDIP.Api.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace NNDIP.Api.Controllers
{
    [Authorize]
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public EventController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(OperationId = "GetEvents")]
        public async Task<ActionResult<IEnumerable<SimpleEventDto>>> GetEvents()
        {
            IEnumerable<Event> events = await _repositoryWrapper.EventRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SimpleEventDto>>(events).ToList();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetEvent")]
        public async Task<ActionResult<SimpleEventDto>> GetEvent(long id)
        {
            Event @event = await _repositoryWrapper.EventRepository.GetByIdAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return _mapper.Map<SimpleEventDto>(@event);
        }
    }

}

