using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Support.UI;


/* 
 * 
 * Notes
 * This has been tested on July 4th, 2019 against (OFFICE system)
 * FIREFOX 60.7.2esr (64-bit)
 * CHROME Version 75 (64-bit)
 * IE Version 11
 * 
 * Note that in the lab we must use an older version of ChromeDriver at present.  
 * 
 * C. Mark Yendt (July 2019)
 */

namespace SeleniumKatalonUnitTesting
{
    [TestClass]
    public class KatalonAutomationExample
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;
        //private const string BROWSER = "FIREFOX";
        private const string BROWSER = "CHROME";
        //private const string BROWSER = "IE";
        //private const string BROWSER = "EDGE";

        private const string DRIVER_LOCATION = @"C:\Drivers";
        private const string FIREFOX_BIN_LOCATION = @"C:\Program Files\Mozilla Firefox\firefox.exe";

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            // FIREFOX
            if (BROWSER == "FIREFOX")
            {
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(DRIVER_LOCATION);
                // Note that the line below needs to be the full exe Name not just the path
                service.FirefoxBinaryPath = FIREFOX_BIN_LOCATION;
                driver = new FirefoxDriver(service);   // WORKS 
            }
            else if (BROWSER == "CHROME")
       
            {
                ChromeOptions options = new ChromeOptions();
                options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

                driver = new ChromeDriver(
                    DRIVER_LOCATION,
                    options,
                    TimeSpan.FromSeconds(60)
                );
            }

                    //driver = new ChromeDriver(DRIVER_LOCATION);  // WORKS ! 
            else if (BROWSER == "IE")
                // Internet EXPLORER NOTE : Must add DRIVER_LOCATION to Path
                driver = new InternetExplorerDriver();  // WORKS !
            else if (BROWSER == "EDGE")
                driver = new EdgeDriver(DRIVER_LOCATION);


        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            try
            {
                //driver.Quit();// quit does not close the window
                driver.Close();
                driver.Dispose();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [TestInitialize]
        public void InitializeTest()
        {
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [TestMethod]
        public void RunAllTests()
        {

            // Put your test cases in order here (must be in a specific order.)

            TestLoginAdmin();

            TestCreateUser();

            TestDeleteUser();

            TestCityDirectory("Belleville {2}");
            TestCityDirectory("Cobourg {4}");
            TestCityDirectory("Dartmouth {3}");
            TestCityDirectory("Etobicoke {4}");

        }

        /// <summary>
        /// This method when called initiates the proper sequence of the testing process.
        /// </summary>
        [TestMethod]
        // not labelled with TestMethod because we want to control the flow of test cases.
        public void TestLoginAdmin()
        {
            driver.Navigate().GoToUrl("https://csunix.mohawkcollege.ca/tooltime/comp10066/A3/login.php");
            driver.FindElement(By.Id("username")).Click();
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("adminP6ss");
            driver.FindElement(By.Name("Submit")).Click();
            driver.FindElement(By.Id("loginname")).Click();
            try
            {
                Assert.AreEqual("User: admin", driver.FindElement(By.Id("loginname")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("User Admin", driver.FindElement(By.LinkText("User Admin")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.FindElement(By.Id("loginname")).Click();
            try
            {
                Assert.AreEqual("Not Logged In", driver.FindElement(By.Id("loginname")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        }
        /// <summary>
        /// When called this method will create a normal permissioned active user in the database.
        /// </summary>
        public void TestCreateUser()
        {
            driver.Navigate().GoToUrl("https://csunix.mohawkcollege.ca/tooltime/comp10066/A3/login.php");
            driver.FindElement(By.Id("username")).Click();
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).Click();
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("adminP6ss");
            driver.FindElement(By.Name("searchByPC")).Submit();
            driver.FindElement(By.LinkText("User Admin")).Click();
            driver.FindElement(By.Id("username")).Click();
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("Testuserm1");
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("Testuserm1");
            driver.FindElement(By.Name("activate")).Click();
            driver.FindElement(By.XPath("//form[@id='form1']/table/tbody/tr[5]/td/input[2]")).Click();
            driver.FindElement(By.Name("Add New Member")).Click();
            try
            {
                Assert.AreEqual("Record successfully inserted.", driver.FindElement(By.XPath("//div[@id='body']/div/div")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.XPath("//div[@id='body']/div/table/tbody")).Text, "^[\\s\\S]*Testuserm1[\\s\\S]*$"));
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.FindElement(By.Id("username")).Click();
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("Testuserm1");
            driver.FindElement(By.Name("password")).Click();
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("Testuserm1");
            driver.FindElement(By.Name("Submit")).Click();
            try
            {
                Assert.AreEqual("User: Testuserm1", driver.FindElement(By.Id("loginname")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        /// <summary>
        /// This method when called will delete the user that was created in the prior sequence.
        /// </summary>
        private void TestDeleteUser()
        {
            driver.Navigate().GoToUrl("https://csunix.mohawkcollege.ca/tooltime/comp10066/A3/login.php");
            driver.FindElement(By.Id("username")).Click();
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).Click();
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("adminP6ss");
            driver.FindElement(By.Name("searchByPC")).Submit();
            driver.FindElement(By.LinkText("User Admin")).Click();
            driver.FindElement(By.XPath("//td[@id='Testuserm1']/a/img")).Click();
            driver.FindElement(By.LinkText("here")).Click();
            try
            {
                Assert.AreEqual("User Testuserm1 was successfully deleted.", driver.FindElement(By.XPath("//div[@id='body']/div/div")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.LinkText("Logout")).Click();
            try
            {
                Assert.AreEqual("Not Logged In", driver.FindElement(By.Id("loginname")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        }
        /// <summary>
        /// When called this method ensures the number of companies listed for each city referenced is correctly matching up 1:1.
        /// </summary>
        /// <param name="city">This is the city and amount of companies listed in that city.</param>
        private void TestCityDirectory(string city)
        {
            driver.Navigate().GoToUrl("https://csunix.mohawkcollege.ca/tooltime/comp10066/A3/login.php");
            driver.FindElement(By.Id("username")).Click();
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).Click();
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("adminP6ss");
            driver.FindElement(By.Name("searchByPC")).Submit();
            driver.FindElement(By.LinkText("Directory")).Click();
            driver.FindElement(By.Id("city")).Click();
            new SelectElement(driver.FindElement(By.Id("city"))).SelectByText(city);
            driver.FindElement(By.Name("submit")).Click();
            Char[] myChars = { '{', '}' };
            string[] paramInfo = city.Split(myChars);

            string nameToSearch = paramInfo[0].Trim();             // 'Belleville'
            int companyCount = int.Parse(paramInfo[1]);     // 2
            // Contains provincal data. ie. 'Belleville,ON K8P 9M8' taken from XPATH.
            string[] response;

            for (int i = 1; i <= companyCount; i++)
            {
                string path = $"//*[@class='listresults']/ul[@class='companylist'][{i}]/li[3]";
                response = (driver.FindElement(By.XPath(path)).Text).Split(',');
                try
                {
                    Assert.AreEqual(nameToSearch, response[0]); // 'Belleville'
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
            }
            driver.FindElement(By.LinkText("Logout")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}

