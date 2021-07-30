using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SmokeTests.Utils
{
    /// <summary>
    /// Extended attribute from MsTest's TestCategory attribute to add safety to setting an app to tests
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TestCategoryAppAttribute : TestCategoryBaseAttribute
    {
        public TestCategoryAppAttribute(App app)
        {
            TestCategoryApp = app;
        }

        public override IList<string> TestCategories =>
            new List<string> { Enum.GetName(typeof(App), TestCategoryApp) };

        public App TestCategoryApp { get; }
    }

    public enum App
    {
        Mas,
        Sm,
        TransactionApi
    }
}
