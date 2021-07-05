using AutoMapper;
using GuessingGame.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame.Models
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //User answer result
            CreateMap<UserAnswerResultDTO, UserAnswerResultModel>();

            //leaderboard
            CreateMap<UserRankDTO, UserRankModel>();
        }
    }
}
