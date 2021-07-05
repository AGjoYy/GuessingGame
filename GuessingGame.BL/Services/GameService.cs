using GuessingGame.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessingGame.BL.Services
{
    public interface IGameService
    {
        int GenerateRandomNumber();

        UserAnswerResultDTO CheckUserAnswer(string correctNumber, string userNumber);
    }
    public class GameService : IGameService
    {
        public int GenerateRandomNumber()
        {
            var random = new Random();

            IEnumerable<int> numbers = Enumerable.Range(1, 9); // 1 - 9
            int index = random.Next(0, 9); // 0 - 8

            var randomNumbers = new int[4] { -1, -1, -1, -1 };
            for (int i = 0; i < randomNumbers.Count(); i++)
            {
                randomNumbers[i] = numbers.ElementAt(index);

                numbers = Enumerable.Range(0, 10).Except(randomNumbers);
                index = random.Next(0, 10 - (i + 1));
            }

            return Convert.ToInt32(string.Join("", randomNumbers));
        }

        public UserAnswerResultDTO CheckUserAnswer(string correctNumber, string userNumber)
        {
            var correctNumberArray = correctNumber.ToCharArray();
            var userNumberArray = userNumber.ToCharArray();

            var userAnswerResult = new UserAnswerResultDTO();

            if (correctNumber == userNumber)
            {
                userAnswerResult.IsWinner = true;
                userAnswerResult.CorrectDigitsOnTheRightPlace = correctNumberArray.Count();

                return userAnswerResult;
            }


            for (int i = 0; i < userNumberArray.Length; i++)
            {
                if (correctNumberArray.Contains(userNumberArray[i]))
                {
                    if (correctNumberArray[i] == userNumberArray[i])
                        userAnswerResult.CorrectDigitsOnTheRightPlace++;
                    else
                        userAnswerResult.CorrectDigitsNotOnTheRightPlace++;
                }
            }
            return userAnswerResult;
        }
    }
}
