using System;
using FluentAssertions;
using RaspberryPi.Process;
using RaspberryPi.Services;
using Xunit;

namespace RaspberryPi.NET.Tests.Services
{
    public class CommandLineInvocationTests
    {
        [Theory]
        [ClassData(typeof(CommandLineInvocationInvalidTestData))]
        public void ShouldCreateCommandLineInvocation_ThrowsArgumentException(string commandLine, Type exceptionType)
        {
            // Act
            Action action = () => new CommandLineInvocation(commandLine);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        public class CommandLineInvocationInvalidTestData : TheoryData<string, Type>
        {
            public CommandLineInvocationInvalidTestData()
            {
                this.Add(null, typeof(ArgumentException));
                this.Add(@"", typeof(ArgumentException));
                this.Add(@"""""", typeof(ArgumentException));
            }
        }

        [Theory]
        [ClassData(typeof(CommandLineInvocationValidTestData))]
        public void ShouldCreateCommandLineInvocation(string commandLine, string expectedExecutable, string expectedArguments)
        {
            // Act
            var commandLineInvocation = new CommandLineInvocation(commandLine);

            // Assert
            commandLineInvocation.Executable.Should().Be(expectedExecutable);
            commandLineInvocation.Arguments.Should().Be(expectedArguments);
        }

        public class CommandLineInvocationValidTestData : TheoryData<string, string, string>
        {
            public CommandLineInvocationValidTestData()
            {
                this.Add(@"executable", "executable", "");
                this.Add(@"executable -arg1 -arg2", "executable", "-arg1 -arg2");
                this.Add(@"  executable  -arg1 -arg2  ", "executable", "-arg1 -arg2");
                this.Add(@"""executable"" -arg1 -arg2", @"""executable""", "-arg1 -arg2");
                this.Add(@"""path\with spaces\executable"" -arg1 -arg2", @"""path\with spaces\executable""", "-arg1 -arg2");
            }
        }

    }
}
