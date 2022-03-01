namespace hamitsarmis.activitywebsite.backend.Helpers
{
    public class PaginationParams
    {
        private const int MaxPageSize = 500;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        private int _id;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public DateTime? StartDate { get; set; } = DateTime.MinValue;
        public DateTime? EndDate { get; set; } = DateTime.MaxValue;
    }
}
