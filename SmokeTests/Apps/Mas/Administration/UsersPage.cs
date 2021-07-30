using Atata;
using Atata.KendoUI;
using Microsoft.Extensions.Logging;

namespace SmokeTests.Apps.Mas.Administration
{
    using _ = UsersPage;

    [Url("Administration/Users")]
    public class UsersPage : BasePage<_>, IUsersPage
    {
        [FindById("create")]
        public Button<_> NewUserButton { get; private set; }

        public KendoGrid<UserRow, _> Entries { get; private set; }

        public UsersPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IUsersPage Authenticate(string username, string password)
        {
            _logger.LogInformation("Logging into the MAS user create page...");
            return base.Authenticate<IUsersPage>(username, password);
        }

        public IUsersCreatePage NewUser()
        {
            return base.ClickAndGetPage<IUsersCreatePage>(NewUserButton);
        }

        public int NumberOfEntries()
        {
            var rowCount = Entries.Rows.Count.Value;
            _logger.LogInformation($"Found {rowCount} users in the grid.");
            return rowCount;
        }

        public class UserRow : KendoGridRow<_>
        {
            //TODO: Add toggle dropdown for Action column
            public Text<_> FirstName { get; private set; }
            public Text<_> LastName { get; private set; }
            public Text<_> EmailAddress { get; private set; }

            [Term("2FA")]
            public Text<_> TwoFA { get; private set; }
            public Text<_> Roles { get; private set; }
            public Text<_> Groups { get; private set; }
            public Text<_> Status { get; private set; }
        }

        private readonly ILogger _logger;
    }
}
