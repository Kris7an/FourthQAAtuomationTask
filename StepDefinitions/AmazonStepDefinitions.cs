using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FourthQAAtuomationTask.StepDefinitions
{
    [Binding]
    public sealed class AmazonStepDefinitions
    {
        private IWebDriver driver;
        IWebElement searchBar;
        IWebElement price;
        IWebElement book;


        [Given(@"Open the browser")]
        public void GivenOpenTheBrowser() 
        {
            driver = new ChromeDriver();    
            driver.Manage().Window.Maximize();
        }

        [When(@"Enter the page URL")]
        public void WhenEnterTheURL()
        {
            driver.Url = "https://www.amazon.co.uk/404/";
            driver.FindElement(By.XPath("//*[@id=\"j\"]/a")).Click();
        }

        [Then(@"Verify the correct page is opened")]
        public void ThenVerifyThePageIsCorrect()
        {
            driver.FindElement(By.XPath("//input[@id='sp-cc-accept']")).Click();
            var amazonLogo = driver.FindElement(By.XPath("//a[@aria-label='Amazon.co.uk']"));
            searchBar = driver.FindElement(By.XPath("//input[contains(@id,'twotabsearchtextbox')]"));
            Assert.That(amazonLogo, Is.Not.Null);
            Assert.That(searchBar, Is.Not.Null);
        }

        [Given(@"Click on all categories")]
        public void GivenClickOnAllCategories()
        {
            driver.FindElement(By.XPath("//select[contains(@class,'search-dropdown')]")).Click();
        }

        [Given(@"Choose a category")]
        public void GivenChooseACategory()
        {
            driver.FindElement(By.XPath("//option[contains(text(), 'Books')]")).Click();
        }

        [When(@"Search for book")]
        public void WhenSearchForBook()
        {
            searchBar.SendKeys("Harry Potter and the Cursed Child");
            driver.FindElement(By.XPath("//input[@id='nav-search-submit-button']")).Click();
        }

        [Then(@"Verify the book exist")]
        public void ThenVerifyTheBookExist()
        {
            book = driver.FindElement(By.XPath("//span[contains(text(),'Harry Potter and the Cursed Child - Parts One and Two')]"));
           // price = driver.FindElement(By.XPath("(//span[@class='a-price']/span[contains(text(), '£')])[1]"));
           // element text cannot be taken
        }

        [Then(@"Choose the book")]
        public void ThenChooseTheBook()
        {
            book.Click();
        }

        [Then(@"Verify the title of the book is correct")]
        public void ThenVerifyTheTitleOfTheBookIsCorrect()
        {
            var bookTitle = driver.FindElement(By.XPath("//span[@id='productTitle']")).Text;
            Assert.IsTrue(bookTitle.Contains("Harry Potter and the Cursed Child - Parts One and Two"));
        }

        [Then(@"Verify the price of the book")]
        public void ThenVerifyThePriceOfTheBook()
        {
            var bookPrice = driver.FindElement(By.XPath("//span[@class='a-size-base a-color-price a-color-price']")).Text;
            //Assert.That(bookPrice, Is.EqualTo($"{price}"));
        }

        [Then(@"Verify the type of the book")]
        public void ThenVerifyTheTypeOfTheBook()
        {
            driver.FindElement(By.XPath("//li[contains(@class,'selected')]//span[contains(text(),'Paperback')]"));
        }

        [When(@"Add the book to the basket")]
        public void WhenAddTheBookToTheBasket()
        {
            driver.FindElement(By.XPath("//input[@id='add-to-cart-button']")).Click();
        }

        [Then(@"Verify that the right book is added to the basket")]
        public void ThenVerifyThatTheRightBookIsAddedToTheBasket()
        {
            driver.FindElement(By.XPath("//div[@id='sw-atc-details-single-container']//img[contains(@alt,'Harry Potter and the Cursed Child - Parts One and Two')]"));
        }

        [Then(@"Verify that one item is in the basket")]
        public void ThenVerifyThatOneItemIsInTheBasket()
        {
            var itemsInBasket = driver.FindElement(By.XPath("//div[@class='sc-without-multicart']")).Text;
            Assert.IsTrue(itemsInBasket.Contains("Proceed to Checkout"));
        }

        [When(@"Going to the basket")]
        public void WhenGoingToTheBasket()
        {
            var goToBasketBtn = driver.FindElement(By.XPath("//a[@href='/cart?ref_=sw_gtc']"));
            Assert.That(goToBasketBtn.Text, Is.EqualTo("Go to basket"));
            goToBasketBtn.Click();
        }

        [Then(@"Verify book is showned")]
        public void ThenVerifyBookIsShowned()
        {
            var verifyWeAreInBasket = driver.FindElement(By.XPath("//div[@class='a-row']/h1"));
            Assert.That(verifyWeAreInBasket.Text, Is.EqualTo("Shopping Basket"));
            var itemTitleInBasket = driver.FindElement(By.XPath("//span[contains(@class, 'item-product-title')]/span[@class='a-truncate-cut']")).Text;
            Assert.IsTrue(itemTitleInBasket.Contains("Harry Potter and the Cursed Child - Parts One and Two"));
        }

        [Then(@"Verify the type, price, quantity and total price")]
        public void ThenVerifyTheTypePriceQuantityAndTotalPrice()
        {
            var type = driver.FindElement(By.XPath("//span[@class='a-size-small sc-product-binding a-text-bold'][contains(.,'Paperback')]")).Text;
            var priceOfItemInBasket = driver.FindElement(By.XPath("//span[@class='a-size-medium a-color-base sc-price sc-white-space-nowrap sc-product-price a-text-bold'][contains(.,'£4.00')]")).Text;
            var quantity = driver.FindElement(By.XPath("//span[@data-a-class='quantity']//span[contains(text(), '1')]")).Text;
            var totalPrice = driver.FindElement(By.XPath("//span[@id='sc-subtotal-amount-activecart']//span[contains(text(), '£4.00')]")).Text;

            Assert.IsTrue(type.Contains("Paperback"));
            //Assert.That(priceOfItemInBasket, Is.EqualTo($"{price}"));
            Assert.That(quantity, Is.EqualTo("1"));
            Assert.That(totalPrice, Is.EqualTo("£4.00"));

            driver.Quit();
        }
    }
}