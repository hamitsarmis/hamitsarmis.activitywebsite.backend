using Microsoft.EntityFrameworkCore.Infrastructure;

namespace hamitsarmis.activitywebsite.backend.Entities
{
    public class Event
    {
        private ICollection<EventSubscription>? _subscriptions;

        public int Id { get; set; }

        public string? Title { get; set; }

        public virtual ICollection<EventSubscription>? Subscriptions
        {
            get => _subscriptions;
            set => _subscriptions = value;
        }

        public DateTime ExpirationDate { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

    }
}
