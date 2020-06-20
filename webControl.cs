using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ConsoleApp1
{
    public class webControl
    {
        IWebDriver driver = new ChromeDriver();
        public string url{ get; set; }
        public List<int> date{ get; set; }

        public webControl(string url, List<int> list)
        {
            this.url = url;
            this.date = list;
            this.Init();
        }
        public void Init()
        {
            //開啟網頁
            driver.Navigate().GoToUrl(url);
            //隱式等待 - 直到畫面跑出資料才往下執行
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);
        }
        public void SearchDate()
        {
            //找起始日期輸入框
            IWebElement StartDate = driver.FindElement(By.XPath("/html/body/form/div[7]/div[3]/div[4]/div/div[1]/div[2]/div[2]/input[1]"));
            Thread.Sleep(400);
            StartDate.Clear();
            Thread.Sleep(400);
            StartDate.SendKeys($"{this.date[0]}-{this.date[1]}-02\n");
            Thread.Sleep(400);
            //點數據表
            IWebElement RadioButton = driver.FindElement(By.XPath("html/body/form/div[7]/div[3]/div[4]/div/div[1]/div[3]/div/img[2]"));
            Thread.Sleep(400);
            RadioButton.Click();
            Thread.Sleep(400);
            //搜尋
            IWebElement calendarButton = driver.FindElement(By.XPath("/html/body/form/div[7]/div[3]/div[4]/div/div[1]/div[4]/a"));
            Thread.Sleep(400);
            calendarButton.Click();
            Thread.Sleep(400);
        }
        public decimal GetThisMonthRate()
        {
            ReadOnlyCollection<IWebElement> rateList = driver.FindElements(By.ClassName("lastTd"));
            Thread.Sleep(400);
            decimal rate = Convert.ToDecimal(rateList[1].Text);
            return rate;
        }
        public void Quit()
        {
            driver.Quit();
        }
    }
}
