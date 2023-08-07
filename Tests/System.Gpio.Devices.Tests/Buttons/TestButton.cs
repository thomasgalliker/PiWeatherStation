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
            : this(TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(2000), debounceTime)
        {
        }
        
        public TestButton(TimeSpan doublePressTime, TimeSpan holdingTime, TimeSpan debounceTime)
            : base(doublePressTime, holdingTime, debounceTime)
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