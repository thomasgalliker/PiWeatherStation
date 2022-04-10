using System;
using DisplayService.Services.Scheduling;
using Xunit;

namespace DisplayService.Tests.Services.Scheduling
{
    public class CronExpressionTests
    {
        [Fact]
        public void WhenOnlyAsterix_GetDefaultTime()
        {
            // Act
            var expression = CronExpression.Parse("* * * * * *");

            // Assert
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
            // Act
            var expression = CronExpression.Parse("5 12 1 2 0 2018");

            // Assert
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
            // Act
            var expression = CronExpression.Parse("5 12 1 2 0");

            //Assert
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
            // Act
            var expression = CronExpression.Parse("5-8 * * * * *");

            // Assert
            Assert.Equal("5,6,7,8", expression.Minutes);
        }

        [Fact]
        public void WhenFiveAndEightIsSet_GetAllMinutesFiveAndEight()
        {
            // Act
            var expression = CronExpression.Parse("5,8 * * * * *");

            // Assert
            Assert.Equal("5,8", expression.Minutes);
        }

        [Fact]
        public void WhenAsterixSlashIsSet_GetMinutesWithCorrectNumberOfMinutesBetweenThem()
        {
            // Act
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

            //Assert
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

        [Fact]
        public void GivenOnlyMinute_WhenMinuteIsNow_ThenGetNextHourMatch()
        {
            var now = new DateTime(2018, 09, 29, 20, 30, 15);

            var cronExpression = CronExpression.Parse("30 * * * * *");

            var nextTimeToRun = cronExpression.GetNextTimeToRun(now);

            Assert.Equal(new DateTime(2018, 09, 29, 21, 30, 00), nextTimeToRun);
        }

        [Fact]
        public void GivenOnlyMinute_WhenMinuteExistLaterInThisHour_ThenGetAFewMinutes()
        {
            var now = new DateTime(2018, 09, 29, 20, 22, 15);

            var cronExpression = CronExpression.Parse("30 * * * * *");

            var nextTimeToRun = cronExpression.GetNextTimeToRun(now);

            Assert.Equal(new DateTime(2018, 09, 29, 20, 30, 00), nextTimeToRun);
        }

        [Fact]
        public void GivenOnlyMinute_WhenMinuteExistInNextHour_ThenGetMinutesUntilNextHourMatch()
        {
            var now = new DateTime(2018, 09, 29, 20, 22, 15);

            var cronExpression = CronExpression.Parse("20 * * * * *");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Equal(new TimeSpan(0, 57, 45), timeToNext);
        }

        [Fact]
        public void GivenOnlyMinute_WhenMinuteIsZero_ThenGetMinutesUntilNextHourMatch()
        {
            var now = new DateTime(2018, 09, 29, 20, 59, 59);

            var cronExpression = CronExpression.Parse("0 * * * * *");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Equal(new TimeSpan(0, 0, 1), timeToNext);
        }

        [Fact]
        public void GivenOnlyMinute_WhenMinuteIsNextDay_ThenGetMinutesUntilNextHourMatchInTheNextDay()
        {
            var now = new DateTime(2018, 09, 29, 23, 30, 00);

            var cronExpression = CronExpression.Parse("10 * * * * *");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Equal(new TimeSpan(0, 40, 0), timeToNext);
        }

        [Fact]
        public void GivenOnlyMinute_WhenMinuteIsNextMonth_ThenGetMinutesUntilNextHourMatchInTheNextDay()
        {
            var now = new DateTime(2018, 09, 30, 23, 30, 00);

            var cronExpression = CronExpression.Parse("10 * * * * *");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Equal(new TimeSpan(0, 40, 0), timeToNext);
        }

        [Fact]
        public void GivenOnlyMinutes_WhenMinutesExistBeforeAndAfterNowInTheHour_ThenGetMinutesUntilNextHourMatchInThisHour()
        {
            var now = new DateTime(2018, 09, 29, 22, 15, 15);

            var cronExpression = CronExpression.Parse("*/10 * * * * *");


            var schedule = CronExpression.Parse("0,5,10,20,40 * * * * *");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Equal(new TimeSpan(0, 4, 45), timeToNext);
        }

        [Fact]
        public void GivenOnlyHour_WhenHourExistWithinTheSameDay_ThenGetTimeUntilHourMatchInThisDay()
        {
            var now = new DateTime(2018, 09, 29, 22, 15, 15);

            var cronExpression = CronExpression.Parse("* 23 * * * *");

            var nextTimeToRun = cronExpression.GetNextTimeToRun(now);

            Assert.Equal(new DateTime(2018, 09, 29, 23, 00, 00), nextTimeToRun);
        }

        [Fact]
        public void GivenOnlyHour_WhenHourExistNextDay_ThenGetTimeUntilHourMatchInNextDay()
        {
            var now = new DateTime(2018, 09, 29, 22, 15, 15);

            var cronExpression = CronExpression.Parse("* 15 * * * *");

            var nextTimeToRun = cronExpression.GetNextTimeToRun(now);

            Assert.Equal(new DateTime(2018, 09, 30, 15, 00, 00), nextTimeToRun);
        }

        [Fact]
        public void GivenOnlyHours_WhenHourExistWithinTheSameDay_ThenGetTimeUntilHourMatchInThisDay()
        {
            var now = new DateTime(2018, 09, 29, 14, 15, 15);

            var cronExpression = CronExpression.Parse("* */6 * * * *");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Equal(new TimeSpan(3, 44, 45), timeToNext);
        }

        [Fact]
        public void GivenOnlyHours_WhenHoursExistBeforeAndAfterNowInTheDay_ThenGetMinutesUntilNextHourMatchInThisDay()
        {
            var now = new DateTime(2018, 09, 29, 22, 15, 15);

            var cronExpression = CronExpression.Parse("* 15,23 * * * *");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Equal(new TimeSpan(0, 44, 45), timeToNext);
        }

        [Fact]
        public void GivenOnlyHour_WhenHourIsNowWithinTheSameDayWithMinutesToGo_ThenGetTimeUntilHourMatchInThisHour()
        {
            var now = new DateTime(2018, 09, 29, 22, 57, 15);

            var cronExpression = CronExpression.Parse("* 22 * * * *");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Equal(new TimeSpan(0, 0, 45), timeToNext);
        }

        [Fact]
        public void GivenOnlyHour_WhenHourIsNowWithinTheSameDayButTheLastMinute_ThenGetTimeUntilHourMatchInTheNextDay()
        {
            var now = new DateTime(2018, 09, 29, 22, 59, 15);

            var cronExpression = CronExpression.Parse("* 22 * * * *");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Equal(new TimeSpan(23, 00, 45), timeToNext);
        }

        [Fact]
        public void GivenOnlyMinuteAndHour_WhenTimeExistsLaterInTheDay_ThenGetTimeUntilNextTimeMatchInThisDay()
        {
            var now = new DateTime(2018, 09, 29, 15, 15, 15);

            var cronExpression = CronExpression.Parse("20 18 * * * *");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Equal(new TimeSpan(3, 4, 45), timeToNext);
        }

        [Fact]
        public void GivenOnlyDayInMonth_WhenDayInMonthIsLaterInTheMonth_ThenGetTimeUntilNextTimeMatchInThisMonth()
        {
            var now = new DateTime(2018, 09, 15, 15, 00, 15);

            var cronExpression = CronExpression.Parse("* * 16 * * *");

            var nextTimeToRun = cronExpression.GetNextTimeToRun(now);

            Assert.Equal(new DateTime(2018, 09, 16, 00, 00, 00), nextTimeToRun);
        }

        [Fact]
        public void GivenOnlyDayInMonth_WheDayIsPreviouslyInSameMonth_ThenGetTimeUntilNextTimeMatchNextMonth()
        {
            var now = new DateTime(2018, 09, 15, 15, 00, 15);

            var cronExpression = CronExpression.Parse("* * 9 * * *");

            var nextTimeToRun = cronExpression.GetNextTimeToRun(now);

            Assert.Equal(new DateTime(2018, 10, 09, 00, 00, 00), nextTimeToRun);
        }

        [Fact]
        public void GivenOnlyDayInMonth_WheDayIsTodayAndNotTheLastMinuteOfTheDay_ThenGetTimeUntilNextTimeMatchToday()
        {
            var now = new DateTime(2018, 09, 15, 15, 00, 15);

            var cronExpression = CronExpression.Parse("* * 15 * * *");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Equal(new TimeSpan(0, 0, 45), timeToNext);
        }

        [Fact]
        public void GivenOnlyDayInMonth_WheDayIsTodayAndTheLastMinuteOfTheDay_ThenGetTimeUntilNextTimeMatch()
        {
            var now = new DateTime(2018, 09, 15, 23, 59, 15);

            var cronExpression = CronExpression.Parse("* * 15 * * *");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Equal(new TimeSpan(29, 0, 0, 45), timeToNext);
        }

        [Fact]
        public void GivenOnlyMonth_WhenMonthIsLaterInTheSameYear_ThenGetTimeUntilMonthMatches()
        {
            var now = new DateTime(2018, 09, 15, 15, 00, 15);

            var cronExpression = CronExpression.Parse("* * * 10 * *");

            var nextTimeToRun = cronExpression.GetNextTimeToRun(now);

            Assert.Equal(new DateTime(2018, 10, 01, 00, 00, 00), nextTimeToRun);
        }

        [Fact]
        public void GivenOnlyMonth_WhenMonthIsPreviouslyInTheYear_ThenGetTimeUntilMonthMatchesNextYear()
        {
            var now = new DateTime(2018, 12, 30, 15, 00, 15);

            var cronExpression = CronExpression.Parse("* * * 1 * *");

            var nextTimeToRun = cronExpression.GetNextTimeToRun(now);

            Assert.Equal(new DateTime(2019, 01, 01, 00, 00, 00), nextTimeToRun);
        }

        [Fact]
        public void GivenOnlyYear_WhenNextYear_ThenGetTimeUntilNextYear()
        {
            var now = new DateTime(2018, 12, 30, 00, 00, 00);

            var cronExpression = CronExpression.Parse("* * * * * 2019");

            var nextTimeToRun = cronExpression.GetNextTimeToRun(now);

            Assert.Equal(new DateTime(2019, 01, 01, 00, 00, 00), nextTimeToRun);
        }

        [Fact]
        public void GivenOnlyYear_WhenPreviousYear_ThenGetNull()
        {
            var now = new DateTime(2018, 12, 30, 00, 00, 00);

            var cronExpression = CronExpression.Parse("* * * * * 2017");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Null(timeToNext);
        }

        [Fact]
        public void GivenOnlyYear_WhenYearIsNow_ThenGetNextMinuteInThisYear()
        {
            var now = new DateTime(2018, 12, 30, 15, 22, 07);

            var cronExpression = CronExpression.Parse("* * * * * 2018");

            var timeToNext = cronExpression.GetTimeToNext(now);

            Assert.Equal(new TimeSpan(0, 0, 53), timeToNext);
        }

        [Fact]
        public void GivenYearAndTimeOfDay_WhenYearIsNow_ThenGetNextMinuteMatchInThisYear()
        {
            var now = new DateTime(2018, 12, 30, 15, 22, 07);

            var cronExpression = CronExpression.Parse("23 15 * * * 2018");

            var timeToNext = cronExpression.GetNextTimeToRun(now);

            Assert.Equal(new DateTime(2018, 12, 30, 15, 23, 00), timeToNext);
        }

        [Fact]
        public void GivenYearMonthDayAndTimeOfDay_WhenYearIsNow_ThenGetNextMinuteMatchInThisYear()
        {
            var now = new DateTime(2018, 06, 15, 15, 22, 07);

            var cronExpression = CronExpression.Parse("23 15 05 08 * 2018");

            var nextTimeToRun = cronExpression.GetNextTimeToRun(now);

            Assert.Equal(new DateTime(2018, 08, 05, 15, 23, 00), nextTimeToRun);
        }
    }
}
