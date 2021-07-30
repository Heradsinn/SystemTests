using Atata;
using Atata.KendoUI;
using Microsoft.Extensions.Logging;
using SmokeTests.Utils;
using VitesseSDK.Models;

namespace SmokeTests.Apps.Mas.MoneyOut
{
    using _ = StoredRecipientCreatePage;

    [Url("StoredRecipient/Create")]
    public class StoredRecipientCreatePage : BasePage<_>, IStoredRecipientCreatePage
    {
        [FindById("Recipient_ExternalStoredRecipientId")]
        public TextInput<_> StoredRecipientId { get; private set; }
        
        [FindByAttribute("aria-owns", "RecipientCountryCode_listbox")]
        public KendoDropDownList<_> RecipientCountry { get; private set; }

        [FindByName("Recipient.Currency_input")]
        public KendoComboBoxInput<_> RecipientCurrency { get; private set; }

        [FindById("Recipient.Name")]
        public TextInput<_> BeneficiaryName { get; private set; }

        [FindById("Recipient.Account.AccountNumber")]
        public TextInput<_> AccountNumber { get; private set; }

        [FindById("Recipient.Account.SortCode")]
        public TextInput<_> SortCode { get; private set; }

        [FindById("next")]
        public Button<_> SaveButton { get; private set; }

        [Term("Cancel")]
        public Link<_> CancelButton { get; private set; }

        public StoredRecipientCreatePage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IStoredRecipientCreatePage Authenticate(string username, string password)
        {
            return base.Authenticate<IStoredRecipientCreatePage>(username, password);
        }

        public IStoredRecipientCreatePage EnterRecipientDetails(StoredRecipient storedRecipient)
        {
            var countryName = storedRecipient.Country.GetCountryName();
            _logger.LogInformation($"'{storedRecipient.Country}' country name determined as '{countryName}'");

            // Enter initial info
            StoredRecipientId.Set(storedRecipient.ExternalStoredRecipientId);
            RecipientCountry.Set(countryName);
            RecipientCurrency.Select(storedRecipient.Currency.ToString());

            // TODO: enter form dynamically based on incoming country/currency. Maybe using the rules API.
            // ... Currently just handling the miminum for the smoke test - GB/GBP

            // Enter beneficiary personal info
            BeneficiaryName.Set(storedRecipient.Name);

            // Enter beneficiary account info
            AccountNumber.Set(storedRecipient.Account.AccountNumber);
            return SortCode.Set(storedRecipient.Account.SortCode);
        }

        public IStoredRecipientsPage Save()
        {
            _logger.LogInformation($"Saving stored recipient...");
            return base.ClickAndGetPage<IStoredRecipientsPage>(SaveButton);
        }

        public IStoredRecipientsPage Cancel()
        {
            _logger.LogInformation($"Canceling creation of stored recipient...");
            return base.ClickAndGetPage<IStoredRecipientsPage>(CancelButton);
        }

        private readonly ILogger _logger;
    }
}
