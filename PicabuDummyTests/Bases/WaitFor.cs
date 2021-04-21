using OpenQA.Selenium;
using System;
using WaitHelpers = SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;


namespace PicabuDummyTests.Bases
{
    class WaitFor : ItemsBasis
    {
        public const int TIMEOUT_4_SECONDS = 4;
        public const int TIMEOUT_10_SECONDS = 10;
        public const int TIMEOUT_30_SECONDS = 30;
        public const int TIMEOUT_120_SECONDS = 120;

        private IWebDriver driver;
        private WebDriverWait wait;

        public WaitFor(IWebDriver driver, int? timeout = null)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout ?? TIMEOUT_10_SECONDS));
        }

        private WebDriverWait GetWait(int timeout)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
        }

        public IWebElement UntilElementBeClickable(By locator)
        {
            LogWarning("Wait for the element to be clickable: " + locator.ToString());
            return wait.Until(WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public IWebElement UntilElementBeClickable(IWebElement element)
        {
            LogWarning("Wait for the element to be clickable: ");
            return wait.Until(WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public void UntilElementPresent(By locator)
        {
            LogWarning("Wait for the element to be present: " + locator.ToString());
            wait.Until(WaitHelpers.ExpectedConditions.ElementExists(locator));
        }

        public void UntilAllElementsVisible(By locator)
        {
            LogWarning("Wait for all located elements visible: " + locator.ToString());
            wait.Until(WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
        }

        internal void UntilValueChanged(By locator, string previousValue)
        {
            LogWarning("Wait for the element to be changed: " + locator.ToString());
            wait.Until(driver => !driver.FindElement(locator).Text.Equals(previousValue));
        }

        internal void UntilValueBecomes(By locator, string value)
        {
            LogWarning($"Wait for the element value becomes {value}: " + locator.ToString());
            wait.Until(driver => driver.FindElement(locator).Text.Equals(value));
        }
    }
}
