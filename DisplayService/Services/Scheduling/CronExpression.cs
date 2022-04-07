using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DisplayService.Services.Scheduling
{
    public class CronExpression
    {
        public const string Asterisk = "*";
        private static readonly int[] ValidMinuteSlashes = new[] { 2, 3, 4, 5, 6, 10, 12, 15, 20, 30 };
        private static readonly int[] ValidHourSlashes = { 2, 3, 4, 6, 8, 12 };
        private static readonly IReadOnlyCollection<int> EmptyCollection = new ReadOnlyCollection<int>(new int[0]);

        private readonly IReadOnlyCollection<int> minutes;
        private readonly IReadOnlyCollection<int> hours;
        private readonly IReadOnlyCollection<int> daysOfMonth;
        private readonly IReadOnlyCollection<int> months;
        private readonly IReadOnlyCollection<int> dayOfWeek;
        private readonly IReadOnlyCollection<int> years;

        private CronExpression(
            IReadOnlyCollection<int> minutes,
            IReadOnlyCollection<int> hours,
            IReadOnlyCollection<int> daysOfMonth,
            IReadOnlyCollection<int> months,
            IReadOnlyCollection<int> dayOfWeek,
            IReadOnlyCollection<int> years)
        {
            this.minutes = minutes;
            this.hours = hours;
            this.daysOfMonth = daysOfMonth;
            this.months = months;
            this.dayOfWeek = dayOfWeek;
            this.years = years;
        }

        public string Minutes => this.GetTimeString(this.minutes);

        public string Hours => this.GetTimeString(this.hours);

        public string DayOfMonth => this.GetTimeString(this.daysOfMonth);

        public string Month => this.GetTimeString(this.months);

        public string DayOfWeek => this.GetTimeString(this.dayOfWeek);

        public string Year => this.GetTimeString(this.years);

        private string GetTimeString(IReadOnlyCollection<int> data)
        {
            if (!data.Any())
            {
                return Asterisk;
            }

            return string.Join(",", data);

        }

        public DateTime? GetNextTimeToRun(DateTime now)
        {
            if (this.ShouldRunNow(now)) // Now is not the next time to run
            {
                // Complicated shit
                var nextMinute = now.AddMinutes(1);

                if (this.ShouldRunNow(nextMinute))
                {
                    return new DateTime(nextMinute.Year, nextMinute.Month, nextMinute.Day, nextMinute.Hour, nextMinute.Minute, 0);
                }

                var nextMinuteMatch = this.GetNextMatch(nextMinute);

                return nextMinuteMatch;
            }

            return this.GetNextMatch(now);
        }

        public TimeSpan? GetTimeToNext(DateTime now)
        {
            return this.GetNextTimeToRun(now)?.Subtract(now);
        }

        private DateTime? GetNextMatch(DateTime now)
        {
            // If no changes is required this is the next time of execution
            var next = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);

            // Find the year of next execution
            if (this.years.Any())
            {
                var years = this.years.Where(m => m >= now.Year).ToArray();

                if (!years.Any())
                {
                    return null;
                }
                else if (next.Year != years.First())
                {
                    next = new DateTime(years.First(), 1, 1);
                }
            }

            if (this.months.Any())
            {
                var months = this.months.Where(m => m > now.Month).ToArray();

                int fullMonthsToNext;
                if (!months.Any())
                {
                    fullMonthsToNext = 11 - next.Month + this.months.First();
                }
                else
                {
                    fullMonthsToNext = months.First() - next.Month - 1;
                }
                next = next.AddMonths(fullMonthsToNext);

                var fullDaysToNext = 0;
                while (next.AddDays(fullDaysToNext).Day != 1)
                {
                    fullDaysToNext++;
                }
                fullDaysToNext--;
                next = next.AddDays(fullDaysToNext);
                next = next.AddHours(23 - next.Hour);
                next = next.AddMinutes(60 - next.Minute);
            }

            if (this.daysOfMonth.Any())
            {
                var daysOfMonth = this.daysOfMonth.Where(m => m > now.Day).ToArray();

                var fullDaysToNext = 0;
                if (!daysOfMonth.Any())
                {
                    var nextDay = this.daysOfMonth.First();
                    while (next.AddDays(fullDaysToNext).Day != nextDay)
                    {
                        fullDaysToNext++;
                    }
                    fullDaysToNext--;
                }
                else
                {
                    fullDaysToNext = daysOfMonth.First() - next.Day - 1;
                }
                next = next.AddDays(fullDaysToNext);
                next = next.AddHours(23 - next.Hour);
                next = next.AddMinutes(60 - next.Minute);
            }

            if (this.hours.Any())
            {
                var hours = this.hours.Where(m => m >= now.Hour).ToArray();

                int fullHoursToNext;
                if (!hours.Any())
                {
                    fullHoursToNext = 23 - next.Hour + this.hours.First();
                }
                else
                {
                    fullHoursToNext = hours.First() - next.Hour - 1;
                }

                next = next.AddHours(fullHoursToNext);
                next = next.AddMinutes(60 - next.Minute);
            }

            if (this.minutes.Any())
            {
                var minutes = this.minutes.Where(m => m >= now.Minute).ToArray();

                if (!minutes.Any())
                {
                    next = next.AddMinutes(60 - now.Minute + this.minutes.First());
                }
                else if (next.Minute != minutes.First())
                {
                    next = new DateTime(next.Year, next.Month, next.Day, next.Hour, minutes.First(), 0);
                }
            }

            return next;
        }

        public bool ShouldRunNow(DateTime now)
        {
            if (this.minutes.Any())
            {
                if (!this.minutes.Contains(now.Minute))
                {
                    return false;
                }
            }

            if (this.hours.Any())
            {
                if (!this.hours.Contains(now.Hour))
                {
                    return false;
                }
            }

            if (this.daysOfMonth.Any())
            {
                if (!this.daysOfMonth.Contains(now.Day))
                {
                    return false;
                }
            }

            if (this.months.Any())
            {
                if (!this.hours.Contains(now.Month))
                {
                    return false;
                }
            }

            if (this.dayOfWeek.Any())
            {
                if (!this.dayOfWeek.Contains((int)now.DayOfWeek))
                {
                    return false;
                }
            }

            if (this.years.Any())
            {
                if (!this.years.Contains(now.Year))
                {
                    return false;
                }
            }

            return true;
        }



        public static implicit operator CronExpression(string v) => Parse(v);

        public static implicit operator string(CronExpression h) => h.ToString();


        /// <summary>
        /// minute (0 - 59) hour (0 - 23) day of the month (1 - 31) month (1 - 12) day of the week (0 - 6) (Sunday 0 to Saturday 6) Year 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static CronExpression Parse(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentException("Empty expression is not valid", nameof(expression));
            }

            if (expression.Contains("?") || expression.Contains("L") || expression.Contains("W") || expression.Contains("#"))
            {
                throw new ArgumentOutOfRangeException(nameof(expression), expression, "?, L, W and # is not supported in this implementation");
            }

            var parts = expression.Split(' ');
            if (parts.Length > 5 && parts.Length < 6)
            {
                throw new ArgumentException($"Expression must have five or six parts and not {parts.Length}", nameof(expression));
            }

            var minutes = parts[0];
            var hours = parts[1];
            var dayOfMonth = parts[2];
            var month = parts[3];
            var dayOfWeek = parts[4];

            var year = Asterisk;
            if (parts.Length == 6)
            {
                year = parts[5];
            }

            var cronExpression = new CronExpression(
                ParseString(minutes, nameof(minutes), 60, ValidMinuteSlashes),
                ParseString(hours, nameof(hours), 24, ValidHourSlashes),
                ParseString(dayOfMonth, nameof(dayOfMonth), 31),
                ParseString(month, nameof(month), 12),
                ParseString(dayOfWeek, nameof(dayOfWeek), 6),
                ParseString(year, nameof(year), 9999));

            return cronExpression;
        }

        private static IReadOnlyCollection<int> ParseString(string data, string parameterName, int maxValue, int[] validSlashValues = null)
        {
            if (data == Asterisk)
            {
                return EmptyCollection;
            }

            if (data.Contains('-'))
            {
                var dataRange = data.Split('-');
                var start = int.Parse(dataRange[0]);
                var end = int.Parse(dataRange[1]);

                return Enumerable.Range(start, end - start + 1).ToArray();
            }

            if (data.Contains('/') && validSlashValues != null)
            {
                var dataSlash = data.Split('/');

                if (!dataSlash[0].Equals(Asterisk))
                {
                    throw new ArgumentOutOfRangeException(parameterName, dataSlash[0], "First part in Slash expression must be Asterisk");
                }

                var range = int.Parse(dataSlash[1]);

                if (!validSlashValues.Contains(range))
                {
                    throw new ArgumentOutOfRangeException(parameterName, range, $"Only {string.Join(",", validSlashValues)} is valid");
                }

                var slashRangeResult = new List<int>();
                var iterator = 0;
                while (iterator < maxValue)
                {
                    slashRangeResult.Add(iterator);
                    iterator += range;
                }
                return slashRangeResult;
            }

            var dataParts = data.Split(',');

            var result = new List<int>(dataParts.Length);
            foreach (var dataItem in dataParts)
            {
                result.Add(int.Parse(dataItem));
            }

            return result;
        }
    }
}
