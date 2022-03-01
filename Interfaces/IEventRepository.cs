using Microsoft.AspNetCore.Mvc;
using hamitsarmis.activitywebsite.backend.Entities;
using hamitsarmis.activitywebsite.backend.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hamitsarmis.activitywebsite.backend.Interfaces
{
    public interface IEventRepository
    {
        Task<Event?> GetEventAsync(int id);

        Task<Event?> CreateEvent(Event evnt);

        Task<Event?> UpdateEvent(Event evnt);

        Task<Event?> DeleteEvent(Event evnt);

        Task<PagedList<Event>> GetEvents(PaginationParams paginationParams);

        Task<PagedList<EventSubscription>> GetEventSubscriptions(PaginationParams paginationParams);

        EventSubscription? GetEventSubscription(int eventId, int userId);

        Task<EventSubscription?> RegisterUser(int eventId, int userId);

    }
}