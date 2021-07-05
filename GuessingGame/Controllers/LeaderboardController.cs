using AutoMapper;
using GuessingGame.BL.Services;
using GuessingGame.Model.Entities;
using GuessingGame.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame.Controllers
{
    public class LeaderboardController : BaseController
    {
        private readonly ILeaderboardService leaderboardService;
        private readonly IMapper mapper;

        public LeaderboardController(UserManager<User> userManager, ILeaderboardService leaderboardService, IMapper mapper) : base(userManager)
        {
            this.leaderboardService = leaderboardService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaderboard()
        {
            try
            {
                var user = await GetCurrentUser();

                if (user == null)
                    return NotFound("User not found");

                var currentUserName = user.UserName;
                var leaderboard = await leaderboardService.GetLeaderboard();

                var leaderboardModel = new LeaderboardModel
                {
                    CurrentUserName = currentUserName,
                    Users = leaderboard.Select(us => mapper.Map<UserRankModel>(us)).ToList()
                };

                return Ok(leaderboardModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
