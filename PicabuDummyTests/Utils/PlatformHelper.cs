using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace PicabuDummyTests.Utils
{
    public enum Platforms
    {
        Windows, Linux, MacOS
    }

    class PlatformHelper
    {
        public static Platforms GetPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return Platforms.Windows;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return Platforms.Linux;
            else return Platforms.MacOS;
        }
    }
}
