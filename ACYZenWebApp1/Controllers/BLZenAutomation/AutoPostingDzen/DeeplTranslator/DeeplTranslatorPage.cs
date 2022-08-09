using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ACYZenWebApp1.Controllers.BLZenAutomation.AutoPostingDzen.DeeplTranslator;

public class DeeplTranslatorPage
{
    private static IWebDriver _driver;
    private static WebDriverWait _wait;
    private readonly string _url = "https://www.deepl.com/translator";
    
    public DeeplTranslatorPage(WebDriver driver)
    {
        driver.Url = _url;
    }
    
    public DeeplTranslatorPage(WebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }
    public DeeplTranslatorPage Open()
    {
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl(_url);
        return this;
    }

    public string TranslateText(string postBody)
    {
        /*var sourceLanguage = _wait.Until(e => e.FindElement(By.XPath("//span[@class='lmt__language_select__active__title']")));
        if (sourceLanguage.Text != "Русский")
        {
            sourceLanguage.Click();
            _wait.Until(e => e.FindElement(By.XPath("//input[@type='text']"))).SendKeys("Русский" + "\n");
        }
        else
        {
        }*/
        _wait.Until(e => e.FindElement(By.XPath("//textarea[@aria-labelledby='translation-source-heading']"))).SendKeys(postBody);
        Thread.Sleep(5000);
        string firstTranslatedText = _wait.Until(e => e.FindElement(By.XPath("//div[@id='target-dummydiv']"))).Text;
        return firstTranslatedText;
    }
}