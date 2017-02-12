using System;
using Visyn.Public.VisynApp;

namespace Visyn.Public.Settings
{
    public class CefAppSettings : IVisynAppSettings
    {
        public bool AreValid { get; protected set; } = true;
        public void InitializeDefaultSettings(object context)
        {
            AddressBarVisible = false;
        }

        public bool AddressBarVisible { get; set; }

        public string Url { get; set; }

        public CefAppSettings()
        {
        }

        public CefAppSettings(string url, bool addressBarVisible=false)
        {
            AddressBarVisible = addressBarVisible;
            Url = url;
        }

        public static CefAppSettings DefaultSettings()
        {
            return new CefAppSettings("www.yahoo.com", false);
        }
    }
}
