using hamitsarmis.activitywebsite.backend.Entities;
using hamitsarmis.activitywebsite.backend.Helpers;
using hamitsarmis.activitywebsite.backend.Interfaces;

namespace hamitsarmis.activitywebsite.backend.Data
{
    public class MealRepository : IMealRepository
    {

        private readonly DataContext _context;

        public MealRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Meal?> CreateMeal(Meal meal)
        {
            _context.Meals.Add(meal);
            var result = await _context.SaveChangesAsync();
            if (result > 0) return meal;
            return null;
        }

        public async Task<Meal?> DeleteMeal(Meal meal)
        {
            meal = await _context.Meals.FindAsync(meal.Id);
            if (meal == null) return null;
            _context.Meals.Remove(meal);
            int number = await _context.SaveChangesAsync();
            if (number > 0) return meal;
            return null;
        }

        public async Task<Meal?> GetMeal(int id)
        {
            return await _context.Meals.FindAsync(id);
        }

        public async Task<PagedList<Meal>> GetMeals(PaginationParams paginationParams)
        {
            var query = _context.Meals
                .OrderByDescending(m => m.Id)
                .AsQueryable();

            return await PagedList<Meal>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<Meal?> UpdateMeal(Meal meal)
        {
            Meal existingMeal;
            if ((existingMeal = await _context.Meals.FindAsync(meal.Id)) == null) return null;
            _context.Entry(existingMeal).CurrentValues.SetValues(meal);
            var number = await _context.SaveChangesAsync();
            if (number > 0) return meal;
            return null;
        }
    }
}
