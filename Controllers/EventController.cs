using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using hamitsarmis.activitywebsite.backend.DTOs;
using hamitsarmis.activitywebsite.backend.Entities;
using hamitsarmis.activitywebsite.backend.Helpers;
using hamitsarmis.activitywebsite.backend.Interfaces;

namespace hamitsarmis.activitywebsite.backend.Controllers
{
    public class EventController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("retrieve-events")]
        public async Task<ActionResult<IEnumerable<Event>>> RetriveEvents([FromQuery]
            PaginationParams paginationParams)
        {
            return await _unitOfWork.EventRepository.GetEvents(paginationParams);
        }

        [HttpGet("get-event")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var evnt = await _unitOfWork.EventRepository.GetEventAsync(id);
            if(evnt == null)
                return NotFound();
            return evnt;
        }

        [HttpGet("get-event-subscriptions")]
        public async Task<ActionResult<IEnumerable<EventSubscription>>> GetEventSubscriptions([FromQuery]
            PaginationParams paginationParams)
        {
            return await _unitOfWork.EventRepository.GetEventSubscriptions(paginationParams);
        }

        [HttpPost("create-event")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<Event>> CreateEvent([FromBody] EventDto evnt)
        {
            var result = await _unitOfWork.EventRepository.CreateEvent(_mapper.Map<Event>(evnt));
            if (result != null)
            {
                evnt.Id = result.Id;
                return Ok(evnt);
            }
            return BadRequest();
        }

        [HttpPost("update-event")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<Event>> UpdateEvent([FromBody] EventDto evntDto)
        {
            var evnt = await _unitOfWork.EventRepository.UpdateEvent(_mapper.Map<Event>(evntDto));
            if (evnt != null)
                return Ok(evnt);
            return BadRequest();
        }

        [HttpPost("delete-event")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            var evnt = await _unitOfWork.EventRepository.DeleteEvent(new Event { Id = id });
            if (evnt != null)
                return Ok(evnt);
            return BadRequest();
        }

    }
}
