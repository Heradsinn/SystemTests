using Atata;

namespace SmokeTests.Utils
{
    /// <summary>
    /// This is to give grids a chance to load by looking for the loading icon.
    /// It will wait for it to exist and then disappear but isn't fixed on a time as we don't want to hang around unecessarily.
    /// </summary>
    public class WaitForGridToLoadAttribute : WaitForElementAttribute
    {
        public WaitForGridToLoadAttribute(TriggerEvents on = TriggerEvents.BeforeAccess)
            : base(WaitBy.Class, "k-loading-mask", Until.VisibleThenMissingOrHidden, on)
        {
            // On some pages with quick loading the indicator can even not appear.
            // For such case we can decrease the time of element presence and declare that timeout exception should not be thrown.
            // Meaning if within 2 seconds the element will not appear then OK, continue the execution.
            // Note that this settings can differ depending on specific indicator behavior.
            //TODO: Reduce PresenceTimeout gradually while monitoring test runs success
            PresenceTimeout = 2; // Sets max waiting time in seconds for the presence of an element.
            ThrowOnPresenceFailure = false; // Do not throw exception if indicator is not found.
            RetryInterval = 0.25;
            AbsenceTimeout = 10; // Sets max waiting time in seconds for the absence of an element.
        }
    }
}
