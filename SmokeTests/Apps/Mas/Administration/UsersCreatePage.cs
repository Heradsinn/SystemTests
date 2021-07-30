using Atata;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Mas.Administration
{
    using _ = UsersCreatePage;

    [Url("Administration/Create")]
    public class UsersCreatePage : BasePage<_>, IUsersCreatePage
    {
        public EmailInput<_> EmailAddress { get; private set; }

        [Term("Create")]
        public Button<_> CreateButton { get; private set; }

        [Term("Cancel")]
        public Link<_> CancelButton { get; private set; }

        public UsersCreatePage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IUsersCreatePage Authenticate(string username, string password)
        {
            _logger.LogInformation("Logging into the MAS users page...");
            return base.Authenticate<IUsersCreatePage>(username, password);
        }
        public IUsersPage CreateUser(string email)
        {
            _logger.LogInformation($"Creating a new user with the email {email}...");
            EmailAddress.Set(email);
            return base.ClickAndGetPage<IUsersPage>(CreateButton);
        }

        private readonly ILogger _logger;
    }
}
