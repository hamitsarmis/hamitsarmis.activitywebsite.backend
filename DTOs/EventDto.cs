namespace hamitsarmis.activitywebsite.backend.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
