using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace PicabuDummyTests
{
    class Environment
    {
        public static void Initialize()
        {
            var projectPath = Path.GetFullPath(Assembly.GetExecutingAssembly().Location);
            var env = Path.GetFullPath(Path.Combine(projectPath, @"..\..\..\..\.env"));
            DotNetEnv.Env.Load(env);
        }
        public static string BaseUrl => DotNetEnv.Env.GetString("BASE_URL");
        public static double ImplicitWait => double.Parse(DotNetEnv.Env.GetString("IMPLICIT_WAIT"));
    }
}
