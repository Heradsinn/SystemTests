using Atata;
using Atata.KendoUI;
using OpenQA.Selenium;

namespace SmokeTests.Utils
{
    /// <summary>
    /// Custom Atata control to represent dropdown list but required matching by text contains rather than scrict equals.
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    public class CustomKendoDropDownList<TOwner> : KendoDropDownList<TOwner>
    where TOwner : PageObject<TOwner>
    {
        private const string DropDownListItemXPath =
            ".//*[contains(concat(' ', normalize-space(@class), ' '), ' k-animation-container ')]" +
            "//ul[contains(concat(' ', normalize-space(@class), ' '), ' k-list ')]" +
            "/li";

        protected override IWebElement GetDropDownOption(string value, SearchOptions searchOptions = null)
        {
            // Use this approach for strict finding.
            //return Driver.GetWithLogging(
            //    By.XPath($"{DropDownListItemXPath}/span[1][normalize-space(text())='{value}']").
            //    DropDownOption(value).
            //    With(searchOptions));

            // Use this one for "text contains" approach.
            return Driver.GetWithLogging(
                By.XPath($"{DropDownListItemXPath}[contains(., '{value}')]").
                DropDownOption(value).
                With(searchOptions));
        }
    }
}
