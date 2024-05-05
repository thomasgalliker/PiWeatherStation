namespace System.Device.Buttons
{
    /// <summary>
    /// The different states of a button that is being held.
    /// </summary>
    public enum ButtonHoldingState : long
    {
        /// <summary>Button holding started.</summary>
        Started,

        /// <summary>Button holding completed.</summary>
        Completed,

        /// <summary>Button holding cancelled.</summary>
        Canceled,
    }
}
