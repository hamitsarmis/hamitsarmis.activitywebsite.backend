using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using hamitsarmis.activitywebsite.backend.Entities;
using hamitsarmis.activitywebsite.backend.Helpers;
using hamitsarmis.activitywebsite.backend.Interfaces;

namespace hamitsarmis.activitywebsite.backend.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class MealController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public MealController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get-meals")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Meal>>> RetriveMeals([FromQuery]
            PaginationParams paginationParams)
        {
            return await _unitOfWork.MealRepository.GetMeals(paginationParams);
        }

        [HttpGet("get-meal")]
        [AllowAnonymous]
        public async Task<ActionResult<Meal>> GetMeal(int id)
        {
            return await _unitOfWork.MealRepository.GetMeal(id);
        }

        [HttpPost("create-meal")]
        public async Task<ActionResult<Meal>> CreateMeal(Meal meal)
        {
            var result = await _unitOfWork.MealRepository.CreateMeal(meal);
            if (result != null)
            {
                meal.Id = result.Id;
                return Ok(meal);
            }
            return BadRequest();
        }

        [HttpPost("update-meal")]
        public async Task<ActionResult<Meal>> UpdateMeal([FromBody] Meal meal)
        {
            meal = await _unitOfWork.MealRepository.UpdateMeal(meal);
            if (meal != null)
                return Ok(meal);
            return BadRequest();
        }

        [HttpPost("delete-meal")]
        public async Task<ActionResult<Meal>> DeleteMeal(int id)
        {
            var meal = await _unitOfWork.MealRepository.DeleteMeal(new Meal { Id = id });
            if (meal != null)
                return Ok(meal);
            return BadRequest();
        }

    }
}
