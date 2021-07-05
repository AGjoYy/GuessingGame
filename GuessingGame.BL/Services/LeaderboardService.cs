using GuessingGame.BL.DTO;
using GuessingGame.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame.BL.Services
{
    public interface ILeaderboardService
    {
        Task<List<UserRankDTO>> GetLeaderboard();
    }
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IUnitOfWork repository;
        private readonly IConfiguration configuration;

        public LeaderboardService(IUnitOfWork repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        public async Task<List<UserRankDTO>> GetLeaderboard()
        {
            var users = await repository.UserRepository.GetAll().Where(u => u.PlayedGamesCount >= Convert.ToInt32(configuration.GetSection("GameSettings")["MinimumGamesForLeaderboard"])).ToListAsync();

            var leaderboard = users.Select(u => new UserRankDTO
            {
                UserName = u.UserName,
                SuccessRate = u.PlayedGamesCount == 0 ? 0 : Math.Round((double)u.WonGamesCount / u.PlayedGamesCount * 100, 2),
                TotalTries = u.AttemptsCount
            })
            .OrderByDescending(u => u.SuccessRate)
            .ThenBy(u => u.TotalTries)
            .ToList();

            return leaderboard;
        }
    }
}
