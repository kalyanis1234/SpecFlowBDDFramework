using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace TestKatalonWeSite.StepDefinitions
{
    [Binding]
    public class KatalonShoppingCartStepDefinitions
    {
        private IWebDriver driver;
        private int actualItemCount;

        [BeforeScenario]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
        [Given(@"I am on the website homepage")]
        public void GivenIAmOnTheWebsiteHomepage()
        {
            driver.Navigate().GoToUrl("https://cms.demo.katalon.com");
        }

        [When(@"I add four random items to my cart")]
        public void WhenIAddFourRandomItemsToMyCart()
        {
            var list = driver.FindElements(By.ClassName("ellie-thumb-wrapper"));

            for (int i = 0; i < 4 && i < list.Count; i++)
            {
                var element = list[i];
                element.FindElement(By.ClassName("ajax_add_to_cart")).Click();
            }
        }

        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            var val = driver.FindElement(By.Id("primary-menu")).FindElement(By.TagName("ul")).FindElement(By.TagName("li")).FindElement(By.TagName("a"));
            var href = val.GetAttribute("href");
            if (href.Contains("cart"))
            {
                val.Click();
            }
        }

        [Then(@"I find total four items listed in my cart")]
        public void ThenIFindTotalFourItemsListedInMyCart()
        {
            var elements = driver.FindElements(By.ClassName("woocommerce-cart-form"));
            var tableRow = elements[0].FindElement(By.TagName("table")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
            actualItemCount = tableRow.Count;
            Assert.AreEqual(4, actualItemCount - 1);
        }

        [When(@"I remove a product from the cart")]
        public void WhenIRemoveAProductFromTheCart()
        {
            var elements = driver.FindElements(By.ClassName("woocommerce-cart-form"));
            var tableRow = elements[0].FindElement(By.TagName("table")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));

            foreach (var item in tableRow)
            {
                var isLowest = item;
                var tds = item.FindElements(By.TagName("td"));
                foreach (var t in tds)
                {
                    if (t.GetAttribute("class").Contains("product-remove"))
                    {
                        t.FindElement(By.TagName("a")).Click();
                        break;
                    }
                }
                break;
            }
        }

        [Then(@"the cart should have one less product with count (.*)")]
        public void ThenTheCartShouldHaveOneLessProductWithCount(int expectedCount)
        {
            var elements = driver.FindElements(By.ClassName("woocommerce-cart-form"));
            var tableRow = elements[0].FindElement(By.TagName("table")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
            Assert.AreEqual(expectedCount, tableRow.Count - 1);
        }
       

    }
}
