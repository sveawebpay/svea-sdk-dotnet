using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Svea.WebPay.SDK.Helpers
{
    internal static class VersionHelper
    {

        private static readonly string CachedVersion = GetVersionNumber();

        public static string Version => CachedVersion;

        private static string GetVersionNumber()
        {
            string fullVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString()??"1.0.0";
            return fullVersion.Split('+')[0];
        }

    }
}
