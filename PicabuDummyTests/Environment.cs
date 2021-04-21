using System.IO;
using System.Reflection;


namespace PicabuDummyTests
{
    class Environment
    {
        private static string projectPath;

        public static string chromeReleasesPath => Path.GetFullPath(Path.Combine(projectPath, @"..\..\..\..\BrowsersReleases\chromeReleases.json"));
        public static string firefoxReleasesPath => Path.GetFullPath(Path.Combine(projectPath, @"..\..\..\..\BrowsersReleases\firefoxReleases.json"));
        public static string BaseUrl => DotNetEnv.Env.GetString("BASE_URL");
        public static double ImplicitWait => double.Parse(DotNetEnv.Env.GetString("IMPLICIT_WAIT"));

        public static void Initialize()
        {
            projectPath = Path.GetFullPath(Assembly.GetExecutingAssembly().Location);
            var env = Path.GetFullPath(Path.Combine(projectPath, @"..\..\..\..\.env"));
            DotNetEnv.Env.Load(env);
        }
    }
}
