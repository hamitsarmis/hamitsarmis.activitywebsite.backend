using Microsoft.AspNetCore.Mvc;
using hamitsarmis.activitywebsite.backend.Entities;
using hamitsarmis.activitywebsite.backend.Helpers;
using hamitsarmis.activitywebsite.backend.Interfaces;

namespace hamitsarmis.activitywebsite.backend.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly DataContext _context;

        public EventRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Event?> GetEventAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<PagedList<Event>> GetEvents(PaginationParams paginationParams)
        {
            var query = _context.Events
                .Where(e => e.ExpirationDate >= paginationParams.StartDate &&
                            e.ExpirationDate <= paginationParams.EndDate)
                .OrderByDescending(m => m.Id)
                .AsQueryable();

            return await PagedList<Event>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<PagedList<EventSubscription>> GetEventSubscriptions(PaginationParams paginationParams)
        {
            var query = _context.EventSubscriptions.Where(e => e.Event.Id == paginationParams.Id
                                                    && e.Event.ExpirationDate >= paginationParams.StartDate &&
                                                    e.Event.ExpirationDate <= paginationParams.EndDate)
                .OrderByDescending(m => m.Id)
                .AsQueryable();

            return await PagedList<EventSubscription>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public EventSubscription? GetEventSubscription(int eventId, int userId)
        {
            return _context.EventSubscriptions.Where(a => a.Event.Id == eventId && a.User.Id == userId).SingleOrDefault();
        }

        public async Task<EventSubscription?> RegisterUser(int eventId, int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;
            var evnt = await _context.Events.FindAsync(eventId);
            if (evnt == null) return null;
            EventSubscription? result = null;
            _context.EventSubscriptions.Add(result = new EventSubscription
            {
                User = user,
                Event = evnt,
            });
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Event?> CreateEvent(Event evnt)
        {
            _context.Events.Add(evnt);
            var result = await _context.SaveChangesAsync();
            if (result > 0) return evnt;
            return null;
        }

        public async Task<Event?> UpdateEvent(Event evnt)
        {
            Event existingEvent;
            if ((existingEvent = await _context.Events.FindAsync(evnt.Id)) == null) return null;
            _context.Entry(existingEvent).CurrentValues.SetValues(evnt);
            var number = await _context.SaveChangesAsync();
            if (number > 0) return evnt;
            return null;
        }

        public async Task<Event?> DeleteEvent(Event evnt)
        {
            evnt = await _context.Events.FindAsync(evnt.Id);
            if (evnt == null) return null;
            _context.Events.Remove(evnt);
            int number = await _context.SaveChangesAsync();
            if (number > 0) return evnt;
            return null;
        }
    }
}
