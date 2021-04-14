using Microsoft.Win32;
using PicabuDummyTests.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PicabuDummyTests
{
    public enum BrowserType
    {
        Chrome, Firefox
    }

    public class VersionHelper
    {
        private static readonly string baseWinRegistry = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\";
        private static readonly string chromeWinRegistry = $"{baseWinRegistry}chrome.exe";
        private static readonly string firefoxWinRegistry = $"{baseWinRegistry}firefox.exe";
        private static string registryContainer;

        private static Dictionary<string, string> chromeReleases = new Dictionary<string, string> 
        { 
            { "90", "90.0.4430.24" },
            { "89", "89.0.4389.23" },
            { "88", "88.0.4324.96" },
            { "87", "87.0.4280.88" }
        };

        private static Dictionary<string, string> firefoxReleases = new Dictionary<string, string>
        {
            { "88", "0.29.1" },
            { "89", "89.0.4389.23" },
            { "57", "0.21.0" },
            { "55", "0.20.1" }
        };

        public static string GetLocalVersion(BrowserType _type)
        {
            var platform = PlatformHelper.GetPlatform();
            Dictionary<string, string> releases;
            switch (platform)
            {
                case Platforms.Windows:
                    switch (_type)
                    {
                        case BrowserType.Firefox:
                            registryContainer = firefoxWinRegistry;
                            releases = firefoxReleases;
                            break;
                        default:
                            registryContainer = chromeWinRegistry;
                            releases = chromeReleases;
                            break;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }            
            var path = Registry.GetValue(registryContainer, "", null);
            var version = FileVersionInfo.GetVersionInfo(path.ToString()).FileVersion.Split(".")[0];
            return releases[version];
        }
    }
}
