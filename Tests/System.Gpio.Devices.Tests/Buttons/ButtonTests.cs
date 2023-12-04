using System.Collections.Generic;
using System.Device.Buttons;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace System.Gpio.Devices.Tests.Buttons
{
    public class ButtonTests
    {
        [Fact]
        public async Task If_Button_Is_Once_Pressed_Press_Event_Fires()
        {
            // Arrange
            var pressed = false;
            var holding = false;
            var doublePressed = false;

            var button = new TestButton();

            button.Press += (sender, e) =>
            {
                pressed = true;
            };

            button.Holding += (sender, e) =>
            {
                holding = true;
            };

            button.DoublePress += (sender, e) =>
            {
                doublePressed = true;
            };

            // Act
            button.PressButton();

            // Wait a little bit to mimic actual user behavior.
            await Task.Delay(100);

            button.ReleaseButton();

            // Assert
            Assert.True(pressed);
            Assert.False(holding);
            Assert.False(doublePressed);
        }

        [Fact]
        public async Task If_Button_Is_Held_Holding_Event_Fires()
        {
            // Arrange
            var pressed = false;
            var holding = false;
            var doublePressed = false;

            var button = new TestButton();
            button.IsHoldingEnabled = true;

            button.Press += (sender, e) =>
            {
                pressed = true;
            };

            button.Holding += (sender, e) =>
            {
                holding = true;
            };

            button.DoublePress += (sender, e) =>
            {
                doublePressed = true;
            };

            // Act
            button.PressButton();

            // Wait longer than default holding threshold milliseconds, for the click to be recognized as a holding event.
            await Task.Delay(2100);

            button.ReleaseButton();

            // Assert
            Assert.True(holding, "holding");
            Assert.True(pressed, "pressed");
            Assert.False(doublePressed, "doublePressed");
        }

        [Fact]
        public async Task If_Button_Is_Held_And_Holding_Is_Disabled_Holding_Event_Does_Not_Fire()
        {
            // Arrange
            var pressed = false;
            var holding = false;
            var doublePressed = false;

            var button = new TestButton();
            button.IsHoldingEnabled = false;

            button.Press += (sender, e) =>
            {
                pressed = true;
            };

            button.Holding += (sender, e) =>
            {
                holding = true;
            };

            button.DoublePress += (sender, e) =>
            {
                doublePressed = true;
            };

            // Act
            button.PressButton();

            // Wait longer than default holding threshold milliseconds, for the press to be recognized as a holding event.
            await Task.Delay(2100);

            button.ReleaseButton();

            // Assert
            Assert.True(pressed);
            Assert.False(holding);
            Assert.False(doublePressed);
        }

        [Fact]
        public async Task If_Button_Is_Double_Pressed_DoublePress_Event_Fires()
        {
            // Arrange
            var pressedCounter = 0;
            var holdingCounter = 0;
            var doublePressCounter = 0;

            var button = new TestButton();
            button.IsDoublePressEnabled = true;

            button.Press += (sender, e) =>
            {
                Interlocked.Increment(ref pressedCounter);
            };

            button.Holding += (sender, e) =>
            {
                Interlocked.Increment(ref holdingCounter);
            };

            button.DoublePress += (sender, e) =>
            {
                Interlocked.Increment(ref doublePressCounter);
            };

            // Act
            button.PressButton();
            await Task.Delay(100);
            button.ReleaseButton();

            await Task.Delay(200);

            button.PressButton();
            await Task.Delay(100);
            button.ReleaseButton();

            // Assert
            pressedCounter.Should().Be(2);
            holdingCounter.Should().Be(0);
            doublePressCounter.Should().Be(1);
        }

        [Fact]
        public async Task If_Button_Is_Pressed_Twice_DoublePress_Event_Does_Not_Fire()
        {
            // Arrange
            var pressedCounter = 0;
            var holdingCounter = 0;
            var doublePressCounter = 0;

            var button = new TestButton();
            button.IsDoublePressEnabled = true;

            button.Press += (sender, e) =>
            {
                Interlocked.Increment(ref pressedCounter);
            };

            button.Holding += (sender, e) =>
            {
                Interlocked.Increment(ref holdingCounter);
            };

            button.DoublePress += (sender, e) =>
            {
                Interlocked.Increment(ref doublePressCounter);
            };

            // Act
            button.PressButton();
            await Task.Delay(100);
            button.ReleaseButton();

            await Task.Delay(3000);

            button.PressButton();
            await Task.Delay(100);
            button.ReleaseButton();

            // Assert
            pressedCounter.Should().Be(2);
            holdingCounter.Should().Be(0);
            doublePressCounter.Should().Be(0);
        }

        [Fact]
        public async Task If_Button_Is_Double_Pressed_And_DoublePress_Is_Disabled_DoublePress_Event_Does_Not_Fire()
        {
            // Arrange
            var pressed = false;

            var button = new TestButton();
            button.IsDoublePressEnabled = false;

            button.DoublePress += (sender, e) =>
            {
                pressed = true;
            };

            // Act
            button.PressButton();

            // Wait a little bit to mimic actual user behavior.
            await Task.Delay(100);

            button.ReleaseButton();

            // Wait shorter than default double press threshold milliseconds, for the press to be recognized as a double press event.
            await Task.Delay(200);

            button.PressButton();

            // Wait a little bit to mimic actual user behavior.
            await Task.Delay(100);

            button.ReleaseButton();

            // Assert
            Assert.False(pressed);
        }

        [Fact]
        public void If_Button_Is_Pressed_Too_Fast_Debouncing_Removes_Events()
        {
            // Arrange
            var buttonDownCounter = 0;
            var buttonUpCounter = 0;
            var pressedCounter = 0;
            var holdingCounter = 0;
            var doublePressCounter = 0;

            var button = new TestButton(
                doublePressTime: TimeSpan.FromMilliseconds(30000), 
                holdingTime: TimeSpan.FromMilliseconds(1000), 
                debounceTime: TimeSpan.FromMilliseconds(10000));

            button.ButtonDown += (sender, e) =>
            {
                Interlocked.Increment(ref buttonDownCounter);
            };
            
            button.ButtonUp += (sender, e) =>
            {
                Interlocked.Increment(ref buttonUpCounter);
            };
            
            button.Press += (sender, e) =>
            {
                Interlocked.Increment(ref pressedCounter);
            };

            button.Holding += (sender, e) =>
            {
                Interlocked.Increment(ref holdingCounter);
            };

            button.DoublePress += (sender, e) =>
            {
                Interlocked.Increment(ref doublePressCounter);
            };

            // Act
            button.PressButton();
            button.ReleaseButton();
            button.PressButton();
            button.ReleaseButton();
            button.PressButton();
            button.ReleaseButton();
            button.PressButton();
            button.ReleaseButton();
            button.PressButton();
            button.ReleaseButton();

            // Assert
            buttonDownCounter.Should().Be(1);
            buttonUpCounter.Should().Be(1);
            pressedCounter.Should().Be(1);
            holdingCounter.Should().Be(0);
            doublePressCounter.Should().Be(0);
        }

        /// <summary>
        /// From issue #1877
        /// The problem arises when the button is held down for longer then the debounce timeout.
        /// Then, as it is released there will be a "pressed" event caused by the bounces
        /// happening during release, and the desired "released" event is fired,
        /// due to the debouncing getting started by "pressed"
        /// </summary>
        [Fact]
        public async Task If_Button_Is_Held_Down_Longer_Than_Debouncing()
        {
            // Arrange
            var holding = false;
            var doublePressed = false;
            var buttonDownCounter = 0;
            var buttonUpCounter = 0;
            var pressedCounter = 0;

            // holding is 2 secs, debounce is 1 sec
            var button = new TestButton(TimeSpan.FromMilliseconds(1000));
            button.IsHoldingEnabled = true;

            button.Press += (sender, e) =>
            {
                Interlocked.Increment(ref pressedCounter);
            };

            button.ButtonDown += (sender, e) =>
            {
                Interlocked.Increment(ref buttonDownCounter);
            };

            button.ButtonUp += (sender, e) =>
            {
                Interlocked.Increment(ref buttonUpCounter);
            };

            button.Holding += (sender, e) =>
            {
                holding = true;
            };

            button.DoublePress += (sender, e) =>
            {
                doublePressed = true;
            };

            // Act
            // pushing the button. This will trigger the buttonDown event
            button.PressButton();
            await Task.Delay(2200);
            // releasing the button. This will trigger the pressed and buttonUp event
            button.ReleaseButton();

            // now simulating hw bounces which should not be detected
            button.PressButton();
            button.ReleaseButton();
            button.PressButton();
            button.ReleaseButton();
            button.PressButton();
            button.ReleaseButton();

            // Assert
            Assert.True(buttonDownCounter == 1, "ButtonDown counter is wrong");
            Assert.True(buttonUpCounter == 1, "ButtonUp counter is wrong");
            Assert.True(pressedCounter == 1, "pressedCounter counter is wrong");
            Assert.True(holding, "holding");
            Assert.False(doublePressed, "doublePressed");
        }
        
        [Fact]
        public async Task If_Button_Is_Held_Down_Longer_HoldingTimer()
        {
            // Arrange
            var holdingEvents = new List<ButtonHoldingEventArgs>();
            var button = new TestButton(TimeSpan.FromMilliseconds(1000));
            button.IsHoldingEnabled = true;

            button.Holding += (sender, e) =>
            {
                holdingEvents.Add(e);
            };

            // Act
            button.PressButton();
            await Task.Delay(2200);
            var isHoldingBeforeRelease = button.IsHolding;
            button.ReleaseButton();
            var isHoldingAfterRelease = button.IsHolding;

            // Assert
            holdingEvents.Should().HaveCount(2);
            holdingEvents.ElementAt(0).HoldingState.Should().Be(ButtonHoldingState.Started);
            holdingEvents.ElementAt(1).HoldingState.Should().Be(ButtonHoldingState.Completed);

            isHoldingBeforeRelease.Should().BeTrue();
            isHoldingAfterRelease.Should().BeFalse();
        }

        [Fact]
        public async Task If_Button_Is_Pressed_Multiple_Times_Without_Release()
        {
            // Arrange
            var buttonDownCounter = 0;
            var buttonUpCounter = 0;

            var button = new TestButton(TimeSpan.FromMilliseconds(50));

            button.ButtonDown += (sender, e) =>
            {
                Interlocked.Increment(ref buttonDownCounter);
            };
            button.ButtonUp += (sender, e) =>
            {
                Interlocked.Increment(ref buttonUpCounter);
            };

            // Act
            button.PressButton();
            await Task.Delay(100);
            button.PressButton();

            // Assert
            button.IsPressed.Should().BeTrue();
            buttonDownCounter.Should().Be(1);
            buttonUpCounter.Should().Be(0);
        }

        [Fact]
        public void If_Button_Is_Released_Without_Being_Pressed()
        {
            // Arrange
            var buttonDownCounter = 0;
            var buttonUpCounter = 0;

            var button = new TestButton(TimeSpan.FromMilliseconds(50));

            button.ButtonDown += (sender, e) =>
            {
                Interlocked.Increment(ref buttonDownCounter);
            };
            button.ButtonUp += (sender, e) =>
            {
                Interlocked.Increment(ref buttonUpCounter);
            };

            // Act
            button.ReleaseButton();

            // Assert
            button.IsPressed.Should().BeFalse();
            buttonDownCounter.Should().Be(0);
            buttonUpCounter.Should().Be(0);
        }

        [Fact]
        public void If_Button_Has_No_DebounceTime()
        {
            // Arrange
            var buttonDownCounter = 0;
            var buttonUpCounter = 0;

            var button = new TestButton(debounceTime: TimeSpan.Zero);

            button.ButtonDown += (sender, e) =>
            {
                Interlocked.Increment(ref buttonDownCounter);
            };
            button.ButtonUp += (sender, e) =>
            {
                Interlocked.Increment(ref buttonUpCounter);
            };

            // Act
            button.PressButton();
            button.ReleaseButton();
            button.PressButton();
            button.ReleaseButton();

            // Assert
            buttonDownCounter.Should().Be(2);
            buttonUpCounter.Should().Be(2);
        }

        [Fact]
        public async Task If_Button_Is_Disposed_HoldingTimer_Should_Be_Reset()
        {
            // Arrange
            var buttonDownCount = 0;
            var buttonUpCount = 0;
            var holdingCounter = 0;

            var button = new TestButton();
            button.IsHoldingEnabled = true;

            button.ButtonDown += (sender, e) =>
            {
                Interlocked.Increment(ref buttonDownCount);
            };
            
            button.ButtonUp += (sender, e) =>
            {
                Interlocked.Increment(ref buttonUpCount);
            };

            button.Holding += (sender, e) =>
            {
                Interlocked.Increment(ref holdingCounter);
            };

            // Act
            button.PressButton();
            await Task.Delay(500);
            button.Dispose();
            await Task.Delay(1600);
            button.ReleaseButton();

            // Assert
            buttonDownCount.Should().Be(1);
            buttonUpCount.Should().Be(1);
            holdingCounter.Should().Be(0);
        }
    }
}