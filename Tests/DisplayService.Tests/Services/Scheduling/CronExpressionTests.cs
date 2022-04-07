using System;
using DisplayService.Services.Scheduling;
using Moq;
using Xunit;

namespace DisplayService.Tests.Services.Scheduling
{
    public class CronExpressionTests
    {

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

        //[Fact]
        //public void GivenOnlyMinute_WhenMinuteExistInNextHour_ThenGetMinutesUntilNextHourMatch()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 29, 20, 22, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("20 * * * * *");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Equal(new TimeSpan(0, 57, 45), timeToNext);
        //}

        //[Fact]
        //public void GivenOnlyMinute_WhenMinuteIsZero_ThenGetMinutesUntilNextHourMatch()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 29, 20, 59, 59));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("0 * * * * *");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Equal(new TimeSpan(0, 0, 1), timeToNext);
        //}

        //[Fact]
        //public void GivenOnlyMinute_WhenMinuteIsNextDay_ThenGetMinutesUntilNextHourMatchInTheNextDay()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 29, 23, 30, 00));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("10 * * * * *");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Equal(new TimeSpan(0, 40, 0), timeToNext);
        //}

        //[Fact]
        //public void GivenOnlyMinute_WhenMinuteIsNextMonth_ThenGetMinutesUntilNextHourMatchInTheNextDay()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 30, 23, 30, 00));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("10 * * * * *");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Equal(new TimeSpan(0, 40, 0), timeToNext);
        //}

        //[Fact]
        //public void GivenOnlyMinutes_WhenMinutesExistBeforeAndAfterNowInTheHour_ThenGetMinutesUntilNextHourMatchInThisHour()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 29, 22, 15, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("*/10 * * * * *");


        //    var schedule = cronParser.Parse("0,5,10,20,40 * * * * *");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Equal(new TimeSpan(0, 4, 45), timeToNext);
        //}

        //[Fact]
        //public void GivenOnlyHour_WhenHourExistWithinTheSameDay_ThenGetTimeUntilHourMatchInThisDay()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 29, 22, 15, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* 23 * * * *");

        //    var nextTimeToRun = cronExpression.GetNextTimeToRun();

        //    Assert.Equal(new DateTime(2018, 09, 29, 23, 00, 00), nextTimeToRun);
        //}

        //[Fact]
        //public void GivenOnlyHour_WhenHourExistNextDay_ThenGetTimeUntilHourMatchInNextDay()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 29, 22, 15, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* 15 * * * *");

        //    var nextTimeToRun = cronExpression.GetNextTimeToRun();

        //    Assert.Equal(new DateTime(2018, 09, 30, 15, 00, 00), nextTimeToRun);
        //}

        //[Fact]
        //public void GivenOnlyHours_WhenHourExistWithinTheSameDay_ThenGetTimeUntilHourMatchInThisDay()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 29, 14, 15, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* */6 * * * *");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Equal(new TimeSpan(3, 44, 45), timeToNext);
        //}

        //[Fact]
        //public void GivenOnlyHours_WhenHoursExistBeforeAndAfterNowInTheDay_ThenGetMinutesUntilNextHourMatchInThisDay()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 29, 22, 15, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* 15,23 * * * *");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Equal(new TimeSpan(0, 44, 45), timeToNext);
        //}

        //[Fact]
        //public void GivenOnlyHour_WhenHourIsNowWithinTheSameDayWithMinutesToGo_ThenGetTimeUntilHourMatchInThisHour()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 29, 22, 57, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* 22 * * * *");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Equal(new TimeSpan(0, 0, 45), timeToNext);
        //}

        //[Fact]
        //public void GivenOnlyHour_WhenHourIsNowWithinTheSameDayButTheLastMinute_ThenGetTimeUntilHourMatchInTheNextDay()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 29, 22, 59, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* 22 * * * *");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Equal(new TimeSpan(23, 00, 45), timeToNext);
        //}

        //[Fact]
        //public void GivenOnlyMinuteAndHour_WhenTimeExistsLaterInTheDay_ThenGetTimeUntilNextTimeMatchInThisDay()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 29, 15, 15, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("20 18 * * * *");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Equal(new TimeSpan(3, 4, 45), timeToNext);
        //}

        //[Fact]
        //public void GivenOnlyDayInMonth_WhenDayInMonthIsLaterInTheMonth_ThenGetTimeUntilNextTimeMatchInThisMonth()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 15, 15, 00, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* * 16 * * *");

        //    var nextTimeToRun = cronExpression.GetNextTimeToRun();

        //    Assert.Equal(new DateTime(2018, 09, 16, 00, 00, 00), nextTimeToRun);
        //}

        //[Fact]
        //public void GivenOnlyDayInMonth_WheDayIsPreviouslyInSameMonth_ThenGetTimeUntilNextTimeMatchNextMonth()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 15, 15, 00, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* * 9 * * *");

        //    var nextTimeToRun = cronExpression.GetNextTimeToRun();

        //    Assert.Equal(new DateTime(2018, 10, 09, 00, 00, 00), nextTimeToRun);
        //}

        //[Fact]
        //public void GivenOnlyDayInMonth_WheDayIsTodayAndNotTheLastMinuteOfTheDay_ThenGetTimeUntilNextTimeMatchToday()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 15, 15, 00, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* * 15 * * *");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Equal(new TimeSpan(0, 0, 45), timeToNext);
        //}

        //[Fact]
        //public void GivenOnlyDayInMonth_WheDayIsTodayAndTheLastMinuteOfTheDay_ThenGetTimeUntilNextTimeMatch()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 15, 23, 59, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* * 15 * * *");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Equal(new TimeSpan(29, 0, 0, 45), timeToNext);
        //}

        //[Fact]
        //public void GivenOnlyMonth_WhenMonthIsLaterInTheSameYear_ThenGetTimeUntilMonthMatches()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 09, 15, 15, 00, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* * * 10 * *");

        //    var nextTimeToRun = cronExpression.GetNextTimeToRun();

        //    Assert.Equal(new DateTime(2018, 10, 01, 00, 00, 00), nextTimeToRun);
        //}

        //[Fact]
        //public void GivenOnlyMonth_WhenMonthIsPreviouslyInTheYear_ThenGetTimeUntilMonthMatchesNextYear()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 12, 30, 15, 00, 15));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* * * 1 * *");

        //    var nextTimeToRun = cronExpression.GetNextTimeToRun();

        //    Assert.Equal(new DateTime(2019, 01, 01, 00, 00, 00), nextTimeToRun);
        //}

        //[Fact]
        //public void GivenOnlyYear_WhenNextYear_ThenGetTimeUntilNextYear()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 12, 30, 00, 00, 00));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* * * * * 2019");

        //    var nextTimeToRun = cronExpression.GetNextTimeToRun();

        //    Assert.Equal(new DateTime(2019, 01, 01, 00, 00, 00), nextTimeToRun);
        //}

        //[Fact]
        //public void GivenOnlyYear_WhenPreviousYear_ThenGetNull()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 12, 30, 00, 00, 00));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* * * * * 2017");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Null(timeToNext);
        //}

        //[Fact]
        //public void GivenOnlyYear_WhenYearIsNow_ThenGetNextMinuteInThisYear()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 12, 30, 15, 22, 07));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("* * * * * 2018");

        //    var timeToNext = cronExpression.GetTimeToNext();

        //    Assert.Equal(new TimeSpan(0, 0, 53), timeToNext);
        //}

        //[Fact]
        //public void GivenYearAndTimeOfDay_WhenYearIsNow_ThenGetNextMinuteMatchInThisYear()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 12, 30, 15, 22, 07));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("23 15 * * * 2018");

        //    var timeToNext = cronExpression.GetNextTimeToRun();

        //    Assert.Equal(new DateTime(2018, 12, 30, 15, 23, 00), timeToNext);
        //}

        //[Fact]
        //public void GivenYearMonthDayAndTimeOfDay_WhenYearIsNow_ThenGetNextMinuteMatchInThisYear()
        //{
        //    var cronDateTimeServiceMock = new Mock<ICronDateTimeService>();
        //    cronDateTimeServiceMock.Setup(m => m.GetNow()).Returns(new DateTime(2018, 06, 15, 15, 22, 07));

        //    var cronParser = new CronParser(cronDateTimeServiceMock.Object);
        //    var cronExpression = cronParser.Parse("23 15 05 08 * 2018");

        //    var nextTimeToRun = cronExpression.GetNextTimeToRun();

        //    Assert.Equal(new DateTime(2018, 08, 05, 15, 23, 00), nextTimeToRun);
        //}
    }
}
