using AutoMapper;
using GuessingGame.BL.DTO;
using GuessingGame.BL.Services;
using GuessingGame.Model.Entities;
using GuessingGame.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace GuessingGame.Controllers
{
    public class GameController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly IGameService gameService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public GameController(UserManager<User> userManager, IUserService userService,
            IGameService gameService, IMapper mapper, IConfiguration configuration) : base(userManager)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.gameService = gameService;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        private async Task GenerateAndSaveRandomNumber(User user)
        {
            user.RandomNumber = gameService.GenerateRandomNumber();
            await userManager.UpdateAsync(user);
        }

        [HttpPost]
        public async Task<IActionResult> CheckUserAnswer([FromBody] UserAnswerModel model)
        {
            try
            {
                var user = await GetCurrentUser();

                if (user == null)
                    return NotFound("User not found");

                user.AttemptsCount++;

                if (model.TriesLeft == Convert.ToInt32(configuration.GetSection("GameSettings")["TotalTriesCount"]) - 1) // if player make first try, add games count 
                {
                    await GenerateAndSaveRandomNumber(user);
                    user.PlayedGamesCount++;
                }

                var result = gameService.CheckUserAnswer(user.RandomNumber.ToString(), model.Number);
                int? correctNumber = null;

                if (result.IsWinner)
                {
                    user.WonGamesCount++;
                }
                else if (model.TriesLeft == 0)
                {
                    correctNumber = user.RandomNumber;
                }

                await userService.Update(user);

                var userAnswerResultModel = mapper.Map<UserAnswerResultDTO, UserAnswerResultModel>(result, opt =>
                {
                    opt.AfterMap((src, dest) => dest.CorrectNumber = correctNumber);
                });

                return Ok(userAnswerResultModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GenerateRandomNumber()
        {
            try
            {
                var user = await GetCurrentUser();
                if (user == null)
                    return NotFound("User not found");

                await GenerateAndSaveRandomNumber(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
