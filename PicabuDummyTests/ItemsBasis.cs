using NLog;
using System;


namespace PicabuDummyTests
{
    public class ItemsBasis
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static int _counter = 0;
        protected static string Step
        {
            get
            {
                _counter++;
                return _counter.ToString();
            }
        }

        protected void LogStart(string testName)
        {
            LogManager.Configuration.Variables["tab"] = "\t";
            LogManager.Configuration.Variables["step"] = Step;
            logger.Warn($"{DateTime.Now.ToString()}  *****   Started test: {testName}    *****");
        }

        protected void LogDebug(string msg)
        {
            logger.Debug(msg);
        }

        protected void LogWarning(string msg)
        {
            logger.Warn(msg);
        }

        protected void LogInfo(string msg)
        {
            LogManager.Configuration.Variables["step"] = Step;
            logger.Info(msg);
        }

        protected void LogError(string[] msgs)
        {
            foreach (string msg in msgs)
            {
                logger.Error(msg);
            }
        }

        protected void LogException(Exception e)
        {
            logger.Fatal(e.Message);
            logger.Trace(e.ToString());
            logger.Fatal(e.StackTrace);
            logger.Error(e.Source);
        }

        protected void LogFinish(string testName)
        {
            LogManager.Configuration.Variables["step"] = Step;
            logger.Trace($"{DateTime.Now.ToString()}  *****   Test: {testName} finished    *****");
            _counter = 0;
        }
    }
}
