using DisplayService.Services.Scheduling;
using Xunit;

namespace DisplayService.Tests.Services.Scheduling
{
    public class CronParserTests
    {

        [Fact]
        public void WhenOnlyAsterix_GetDefaultTime()
        {
            var expression = CronExpression.Parse("* * * * * *");

            Assert.Equal("*", expression.Minutes);
            Assert.Equal("*", expression.Hours);
            Assert.Equal("*", expression.DayOfMonth);
            Assert.Equal("*", expression.Month);
            Assert.Equal("*", expression.DayOfWeek);
            Assert.Equal("*", expression.Year);
        }

        [Fact]
        public void WhenRandomTimesIsSet_GetCorrespondingFieldsSet()
        {
            var expression = CronExpression.Parse("5 12 1 2 0 2018");

            Assert.Equal("5", expression.Minutes);
            Assert.Equal("12", expression.Hours);
            Assert.Equal("1", expression.DayOfMonth);
            Assert.Equal("2", expression.Month);
            Assert.Equal("0", expression.DayOfWeek);
            Assert.Equal("2018", expression.Year);
        }

        [Fact]
        public void WhenRandomTimesWithNoYearIsSet_GetCorrespondingFieldsSet()
        {
            var expression = CronExpression.Parse("5 12 1 2 0");

            Assert.Equal("5", expression.Minutes);
            Assert.Equal("12", expression.Hours);
            Assert.Equal("1", expression.DayOfMonth);
            Assert.Equal("2", expression.Month);
            Assert.Equal("0", expression.DayOfWeek);
            Assert.Equal("*", expression.Year);
        }

        [Fact]
        public void WhenFiveToEightIsSet_GetAllMinutesFromFiveToEight()
        {
            var expression = CronExpression.Parse("5-8 * * * * *");

            Assert.Equal("5,6,7,8", expression.Minutes);
        }

        [Fact]
        public void WhenFiveAndEightIsSet_GetAllMinutesFiveAndEight()
        {
            var expression = CronExpression.Parse("5,8 * * * * *");

            Assert.Equal("5,8", expression.Minutes);
        }

        [Fact]
        public void WhenAsterixSlashIsSet_GetMinutesWithCorrectNumberOfMinutesBetweenThem()
        {
            var expression2 = CronExpression.Parse("*/2 * * * * *");
            var expression3 = CronExpression.Parse("*/3 * * * * *");
            var expression4 = CronExpression.Parse("*/4 * * * * *");
            var expression5 = CronExpression.Parse("*/5 * * * * *");
            var expression6 = CronExpression.Parse("*/6 * * * * *");
            var expression10 = CronExpression.Parse("*/10 * * * * *");
            var expression12 = CronExpression.Parse("*/12 * * * * *");
            var expression15 = CronExpression.Parse("*/15 * * * * *");
            var expression20 = CronExpression.Parse("*/20 * * * * *");
            var expression30 = CronExpression.Parse("*/30 * * * * *");

            Assert.Equal("0,2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58", expression2.Minutes);
            Assert.Equal("0,3,6,9,12,15,18,21,24,27,30,33,36,39,42,45,48,51,54,57", expression3.Minutes);
            Assert.Equal("0,4,8,12,16,20,24,28,32,36,40,44,48,52,56", expression4.Minutes);
            Assert.Equal("0,5,10,15,20,25,30,35,40,45,50,55", expression5.Minutes);
            Assert.Equal("0,6,12,18,24,30,36,42,48,54", expression6.Minutes);
            Assert.Equal("0,10,20,30,40,50", expression10.Minutes);
            Assert.Equal("0,12,24,36,48", expression12.Minutes);
            Assert.Equal("0,15,30,45", expression15.Minutes);
            Assert.Equal("0,20,40", expression20.Minutes);
            Assert.Equal("0,30", expression30.Minutes);
        }

    }
}
