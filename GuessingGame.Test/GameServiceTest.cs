using GuessingGame.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GuessingGame.Test
{
    public class GameServiceTest
    {
        [Fact]
        public void Check_RandomNumber_ReturnsEveryDigitIsUnique()
        {
            //ARRANGE
            var gameService = new GameService();
            //ACT
            var number = gameService.GenerateRandomNumber().ToString();
            var everyDigitIsUnique = number.Length == number.Distinct().Count();
            //ASSERT
            Assert.True(everyDigitIsUnique);
        }

        [Fact]
        public void Check_Number8724_Returns_NumberOfCorrectDigitsNotOnTheRightPlace_And_NumberCorrectDigitsOnTheRightPlace()
        {
            //ARRANGE
            var gameService = new GameService();
            //ACT
            var result1 = gameService.CheckUserAnswer(correctNumber: "7046", userNumber: "8724");
            //ASSERT
            Assert.Equal(new List<int> { 2, 0 }, new List<int> { result1.CorrectDigitsNotOnTheRightPlace, result1.CorrectDigitsOnTheRightPlace });
        }

        [Fact]
        public void Check_Number7842_Returns_NumberOfCorrectDigitsNotOnTheRightPlace_And_NumberCorrectDigitsOnTheRightPlace()
        {
            //ARRANGE
            var gameService = new GameService();
            //ACT
            var result2 = gameService.CheckUserAnswer(correctNumber: "7046", userNumber: "7842");
            //ASSERT
            Assert.Equal(new List<int> { 0, 2 }, new List<int> { result2.CorrectDigitsNotOnTheRightPlace, result2.CorrectDigitsOnTheRightPlace });
        }

        [Fact]
        public void Check_Number7640_Returns_NumberOfCorrectDigitsNotOnTheRightPlace_And_NumberCorrectDigitsOnTheRightPlace()
        {
            //ARRANGE
            var gameService = new GameService();
            //ACT
            var result3 = gameService.CheckUserAnswer(correctNumber: "7046", userNumber: "7640");
            //ASSERT
            Assert.Equal(new List<int> { 2, 2 }, new List<int> { result3.CorrectDigitsNotOnTheRightPlace, result3.CorrectDigitsOnTheRightPlace });
        }

        [Fact]
        public void Check_Number0000_Returns_NumberOfCorrectDigitsNotOnTheRightPlace_And_NumberCorrectDigitsOnTheRightPlace()
        {
            //ARRANGE
            var gameService = new GameService();
            //ACT
            var result3 = gameService.CheckUserAnswer(correctNumber: "0000", userNumber: "0000");
            //ASSERT
            Assert.Equal(new List<int> { 0, 4 }, new List<int> { result3.CorrectDigitsNotOnTheRightPlace, result3.CorrectDigitsOnTheRightPlace });
        }
    }
}
