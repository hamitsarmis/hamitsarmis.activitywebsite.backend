using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hamitsarmis.activitywebsite.backend.Entities
{
    public class EventSubscription : IValidatableObject
    {

        public int Id { get; set; }
        public AppUser User { get; set; }
        public Event Event { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Meal? MealChoice { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Email == null && Phone == null)
                yield return new ValidationResult("Either Email or Phone must be specified");
        }
    }
}
