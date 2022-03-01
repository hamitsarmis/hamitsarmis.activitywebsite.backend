using hamitsarmis.activitywebsite.backend.Entities;
using hamitsarmis.activitywebsite.backend.Helpers;

namespace hamitsarmis.activitywebsite.backend.Interfaces
{
    public interface IMealRepository
    {
        Task<Meal?> GetMeal(int id);
        Task<PagedList<Meal>> GetMeals(PaginationParams paginationParams);
        Task<Meal?> CreateMeal(Meal meal);
        Task<Meal?> UpdateMeal(Meal meal);
        Task<Meal?> DeleteMeal(Meal meal);
    }
}
