using System;
using System.Linq.Expressions;
using DisplayService.Services;
using Moq;
using Moq.Language;

namespace DisplayService.Tests.Extensions
{
    internal static class MockExtensions
    {
        /// <summary>
        /// Recursively sets up a new expression.
        /// </summary>
        public static TReturn SetupSequence<TMock, TReturn>(this Mock<TMock> mock, Expression<Func<TMock, TReturn>> expression, TReturn value, Func<TReturn, TReturn> nextValue) where TMock : class
        {
            mock.SetupSequence(expression)
                .Returns(() => SetupSequence(mock, expression, nextValue(value), nextValue));

            return value;
        }
    }
}
