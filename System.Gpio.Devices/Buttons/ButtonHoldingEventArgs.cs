namespace System.Device.Buttons
{
    /// <summary>
    /// Button holding event arguments.
    /// </summary>
    public class ButtonHoldingEventArgs : EventArgs
    {
        /// <summary>
        /// Button holding state.
        /// </summary>
        public ButtonHoldingState HoldingState { get; set; }
    }
}
