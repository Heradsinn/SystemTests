using Atata;

namespace SmokeTests.Utils
{
    /// <summary>
    /// Custom Atata control to represent input boxes that suggest options as the user types.
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    [ControlDefinition("input", ContainingClass = "k-input", ComponentTypeName = "inputdropdown")]
    public class KendoComboBoxInput<TOwner> : Input<string, TOwner>
        where TOwner : PageObject<TOwner>
    {
        [FindByClass("k-list", ScopeSource = ScopeSource.PageObject)]
        public UnorderedList<ListItem<TOwner>, TOwner> DropDownItems { get; private set; }

        public TOwner Select(string value)
        {
            Set(value);
            return DropDownItems[x => x.Content.Value.Contains(value)].Click();
        }
    }
}
