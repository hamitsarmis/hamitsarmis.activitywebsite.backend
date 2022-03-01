using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hamitsarmis.activitywebsite.backend.DTOs;
using hamitsarmis.activitywebsite.backend.Entities;
using hamitsarmis.activitywebsite.backend.Interfaces;
using hamitsarmis.activitywebsite.backend.Extensions;

namespace hamitsarmis.activitywebsite.backend.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(ITokenService tokenService, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

            if (user == null) return Unauthorized("Invalid username");

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserDto
            {
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpPost("register-event")]
        [Authorize]
        public async Task<ActionResult<EventSubscription>> RegisterEvent([FromBody] RegisterDto eventDto)
        {
            var userid = User.GetUserId();
            var evnt = await _unitOfWork.EventRepository.GetEventAsync(eventDto.EventId);
            if (evnt.ExpirationDate < DateTime.UtcNow)
                return BadRequest("Event already expired");
            var evntUsr = _unitOfWork.EventRepository.GetEventSubscription(eventDto.EventId, userid);
            if (evntUsr?.Id != 0) return BadRequest("You have already registered to this event");
            return await _unitOfWork.EventRepository.RegisterUser(eventDto.EventId, userid);
        }
    }
}
