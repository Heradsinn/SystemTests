using System;
using System.Collections.Generic;

namespace SmokeTests.Apps.Mas.Administration
{
    public interface IApiCreateTokenPage : IBasePage
    {
        IApiCreateTokenPage Authenticate(string username, string password);
        IApiPage CreateToken(IList<string> apiRoles, string tokenName = null, DateTime? validFrom = null, DateTime? validTo = null);
    }
}
