using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using RaspberryPi.Internals;
using RaspberryPi.NET.Tests.Logging;
using RaspberryPi.Process;
using RaspberryPi.Services;
using Xunit;
using Xunit.Abstractions;

namespace RaspberryPi.NET.Tests
{
    public class ProcessRunnerTests
    {
        private readonly AutoMocker autoMocker;

        public ProcessRunnerTests(ITestOutputHelper testOutputHelper)
        {
            this.autoMocker = new AutoMocker();
            this.autoMocker.Use<ILogger<ProcessRunner>>(new TestOutputHelperLogger<ProcessRunner>(testOutputHelper));
        }

        [Fact]
        public void ShouldExecuteCommand_Success()
        {
            // Arrange
            var commandLineInvocation = new CommandLineInvocation("dotnet", "--info");

            var processRunner = this.autoMocker.CreateInstance<ProcessRunner>();

            // Act
            CommandLineResult result;
            using (var cancellationTokenSource = new CancellationTokenSource(1000))
            {
                result = processRunner.ExecuteCommand(commandLineInvocation, cancellationTokenSource.Token);
            }

            // Assert
            result.Should().NotBeNull();
            result.OutputData.Should().NotBeEmpty();
            result.ErrorData.Should().BeEmpty();
        }

        [Fact]
        public void ShouldExecuteCommand_Error()
        {
            // Arrange
            var commandLineInvocation = new CommandLineInvocation("dotnet", "--arg");

            var processRunner = this.autoMocker.CreateInstance<ProcessRunner>();
            
            // Act
            var result = processRunner.TryExecuteCommand(commandLineInvocation);

            // Assert
            result.Should().NotBeNull();
            result.OutputData.Should().BeEmpty();
            result.ErrorData.Should().NotBeEmpty();
        }
    }
}