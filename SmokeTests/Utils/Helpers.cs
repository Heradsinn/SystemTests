using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using VitesseSDK.Models;

namespace SmokeTests.Utils
{
    public static class Helpers
    {
        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            var attributes = type.GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : default;
        }

        public static string GetContextProperty(this TestContext context, string property)
        {
            return context.Properties[property]?.ToString() ?? throw new Exception($"Property: '{property}' was not found in runsettings");
        }

        public static string GetCountryName(this Country country)
        {
            var ri = new RegionInfo(country.ToString());
            return ri.EnglishName;
        }
    }
}
