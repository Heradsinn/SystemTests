using Atata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace SmokeTests.Apps.Mas.Administration
{
    using _ = ApiCreateTokenPage;

    [Url("MerchantApiToken/CreateToken")]
    public class ApiCreateTokenPage : BasePage<_>, IApiCreateTokenPage
    {
        public Table<ApiRoleRow, _> Roles { get; private set; }

        [Term("Create")]
        public Button<_> CreateButton { get; private set; }

        [Term("Cancel")]
        public Link<_> CancelButton { get; private set; }

        public ApiCreateTokenPage(IPageFactory pageFactory, ILogger logger) : base(pageFactory, logger)
        {
            _logger = logger;
        }

        public IApiCreateTokenPage Authenticate(string username, string password)
        {
            _logger.LogInformation("Logging into the MAS API Token create page...");
            return base.Authenticate<IApiCreateTokenPage>(username, password);
        }

        public IApiPage CreateToken(IList<string> apiRoles, string tokenName = null, DateTime? validFrom = null, DateTime? validTo = null)
        {
            _logger.LogInformation($"Creating a new token with the following roles: {string.Join(",", apiRoles)}");
            foreach (var apiRole in apiRoles)
                Roles.Rows[x => x.Name.Value == apiRole].Selected.Check();

            return base.ClickAndGetPage<IApiPage>(CreateButton);
        }

        public class ApiRoleRow : TableRow<_>
        {
            [FindByAttribute("type", "checkbox")]
            public CheckBox<_> Selected { get; private set; }
            public Text<_> Name { get; private set; }
            public Text<_> Description { get; private set; }
        }

        private readonly ILogger _logger;
    }
}
