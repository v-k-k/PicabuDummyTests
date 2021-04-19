using OpenQA.Selenium;
using PicabuDummyTests.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicabuDummyTests
{
    partial class BestPage : BasePage
    {
        public readonly By tab = By.XPath("//a[contains(text(),'Лучшее')]/..");

        private readonly By allRead = By.XPath("//section[contains(text(),'Отличная работа, все прочитано!')]");

        private readonly By postsRates = By.XPath("//div[@class='story__rating-count']");

        private readonly By postsDateTimes = By.XPath("//div[@class='story__user-info']//time");

        private readonly By checkedFilter = By.XPath("//span[@class='radio radio_checked']/following-sibling::span");

        private readonly By calendar = By.XPath("//p[@data-role='calendar']");

        private readonly By calendarHead = By.XPath("//div[@class='calendar-head']");

        private readonly By showPostsButton = By.XPath("//button[contains(text(),'Показать посты')]");

        private readonly By animation = By.XPath("//div[@class='stories-feed__spinner']/div[@class='player']/div[@class='player__overlay']");
    }
}
