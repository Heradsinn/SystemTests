using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Sm.MoneyOut
{
    using _ = TransactionRemediationDetailPage;

    [Url("TransactionRemediation/Detail")]
    public class TransactionRemediationDetailPage : BasePage<_>, ITransactionRemediationDetailPage
    {
        [FindById("RemediationComment")]
        public TextArea<_> RemediationComment { get; private set; }

        [FindByName("pass")]
        public Button<_> PassButton { get; private set; }

        public TransactionRemediationDetailPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public ITransactionRemediationDetailPage Authenticate(string username, string password)
        {
            _logger.LogInformation("Navigating to and authenticating TransactionRemediationDetailPage...");
            return Authenticate<_>(username, password);
        }

        public ITransactionRemediationPage PassCARiskCheck(string comment)
        {
            _logger.LogInformation($"Passing the CA risk check with the comment {comment}...");
            RemediationComment.Set(comment);
            return ClickAndGetPage<ITransactionRemediationPage>(PassButton);
        }

        private readonly ILogger _logger;
    }
}
