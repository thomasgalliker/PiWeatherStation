using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace System.Gpio.Devices.Tests.Buttons
{
    public class ButtonTests
    {
        [Fact]
        public async Task If_Button_Is_Once_Pressed_Press_Event_Fires()
        {
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

            button.PressButton();

            // Wait a little bit to mimic actual user behavior.
            await Task.Delay(100);

            button.ReleaseButton();

            Assert.True(pressed);
            Assert.False(holding);
            Assert.False(doublePressed);
        }

        [Fact]
        public async Task If_Button_Is_Held_Holding_Event_Fires()
        {
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

            button.PressButton();

            // Wait longer than default holding threshold milliseconds, for the click to be recognized as a holding event.
            await Task.Delay(2100);

            button.ReleaseButton();

            Assert.True(holding, "holding");
            Assert.True(pressed, "pressed");
            Assert.False(doublePressed, "doublePressed");
        }

        [Fact]
        public async Task If_Button_Is_Held_And_Holding_Is_Disabled_Holding_Event_Does_Not_Fire()
        {
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

            button.PressButton();

            // Wait longer than default holding threshold milliseconds, for the press to be recognized as a holding event.
            await Task.Delay(2100);

            button.ReleaseButton();

            Assert.True(pressed);
            Assert.False(holding);
            Assert.False(doublePressed);
        }

        [Fact]
        public async Task If_Button_Is_Double_Pressed_DoublePress_Event_Fires()
        {
            var pressed = false;
            var holding = false;
            var doublePressed = false;

            var button = new TestButton();
            button.IsDoublePressEnabled = true;

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

            Assert.True(pressed);
            Assert.False(holding);
            Assert.True(doublePressed);
        }

        [Fact]
        public async Task If_Button_Is_Pressed_Twice_DoublePress_Event_Does_Not_Fire()
        {
            var pressed = false;
            var holding = false;
            var doublePressed = false;

            var button = new TestButton();

            button.IsDoublePressEnabled = true;

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

            button.PressButton();

            // Wait a little bit to mimic actual user behavior.
            await Task.Delay(100);

            button.ReleaseButton();

            // Wait longer than default double press threshold milliseconds, for the press to be recognized as two separate presses.
            await Task.Delay(3000);

            button.PressButton();

            // Wait a little bit to mimic actual user behavior.
            await Task.Delay(100);

            button.ReleaseButton();

            Assert.True(pressed);
            Assert.False(holding);
            Assert.False(doublePressed);
        }

        [Fact]
        public async Task If_Button_Is_Double_Pressed_And_DoublePress_Is_Disabled_DoublePress_Event_Does_Not_Fire()
        {
            var pressed = false;

            var button = new TestButton();
            button.IsDoublePressEnabled = false;

            button.DoublePress += (sender, e) =>
            {
                pressed = true;
            };

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

            Assert.False(pressed);
        }

        [Fact]
        public void If_Button_Is_Pressed_Too_Fast_Debouncing_Removes_Events()
        {
            var holding = false;
            var doublePressed = false;
            var pressedCounter = 0;

            var button = new TestButton(TimeSpan.FromMilliseconds(1000));

            button.Press += (sender, e) =>
            {
                pressedCounter++;
            };

            button.Holding += (sender, e) =>
            {
                holding = true;
            };

            button.DoublePress += (sender, e) =>
            {
                doublePressed = true;
            };

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

            Assert.Equal(1, pressedCounter);
            Assert.False(holding);
            Assert.False(doublePressed);
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
                pressedCounter++;
            };

            button.ButtonDown += (sender, e) =>
            {
                buttonDownCounter++;
            };

            button.ButtonUp += (sender, e) =>
            {
                buttonUpCounter++;
            };

            button.Holding += (sender, e) =>
            {
                holding = true;
            };

            button.DoublePress += (sender, e) =>
            {
                doublePressed = true;
            };

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

            Assert.True(buttonDownCounter == 1, "ButtonDown counter is wrong");
            Assert.True(buttonUpCounter == 1, "ButtonUp counter is wrong");
            Assert.True(pressedCounter == 1, "pressedCounter counter is wrong");
            Assert.True(holding, "holding");
            Assert.False(doublePressed, "doublePressed");
        }
    }
}