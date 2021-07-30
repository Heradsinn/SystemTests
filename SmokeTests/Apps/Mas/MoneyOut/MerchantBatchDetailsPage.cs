using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Mas.MoneyOut
{
    using _ = MerchantBatchDetailsPage;

    [Url("MerchantBatchDetails/Payments")]
    public class MerchantBatchDetailsPage : BasePage<_>, IMerchantBatchDetailsPage
    {
        [Term("Request Approval")]
        public Link<_> RequestApprovalButton { get; private set; }

        [Term("Confirm Approval")]
        public Link<_> ConfirmApprovalButton { get; private set; }

        public Link<_> Approve { get; private set; }

        public MerchantBatchDetailsPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IMerchantBatchDetailsPage Authenticate(string username, string password)
        {
            return base.Authenticate<IMerchantBatchDetailsPage>(username, password);
        }

        public IMerchantBatchesPage RequestApproval()
        {
            _logger.LogInformation("Requesting approval for batch...");
            RequestApprovalButton.Click();
            return base.ClickAndGetPage<IMerchantBatchesPage>(ConfirmApprovalButton);
        }

        public IMerchantBatchesPage ApproveBatch()
        {
            _logger.LogInformation("Approving for batch...");
            Approve.Click();
            return base.ClickAndGetPage<IMerchantBatchesPage>(ConfirmApprovalButton);
        }

        private readonly ILogger _logger;
    }
}
