using System.Device.Buttons;

namespace System.Gpio.Devices.Tests.Buttons
{
    public class TestButton : ButtonBase
    {
        public TestButton()
            : base()
        {
        }

        public TestButton(TimeSpan debounceTime)
            : base(TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(2000), debounceTime)
        {
        }

        public void PressButton()
        {
            this.HandleButtonPressed();
        }

        public void ReleaseButton()
        {
            this.HandleButtonReleased();
        }
    }
}