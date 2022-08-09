using ACYZenWebApp1.Controllers.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ACYZenWebApp1.Controllers.BLZenAutomation.Pages;

public class SecurityQuestion : Page
{
    private static IWebDriver _driver;
    private static WebDriverWait _wait;
    private readonly string _url = UrlPassportZen + Endpoints.ChangeKv;
    
    public SecurityQuestion(IWebDriver driver)
    {
        driver.Url = _url;
    }
    
    public SecurityQuestion(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }
    
    public SecurityQuestion Open()
    {
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl(_url);
        return this;
    }
    public void SetKv(string kvAnswer)
    {
        //Установка контрольного вопроса
        var kv = _wait.Until( d => d.FindElement(By.XPath("//select[@class='Select2-Control']")));
        kv.Click();
        var kv1 = _driver.FindElement(By.XPath("//option[@value='14']"));
        kv1.Click();
        var kVsend = _driver.FindElement(By.XPath("//input[@data-t='field:input-updatedAnswer']"));
        kVsend.SendKeys(kvAnswer);
        var kvok = _wait.Until( d => d.FindElement(By.XPath("//button[@class='Button2 Button2_size_l Button2_view_action Button2_type_submit']")));
        kvok.Click();
        Thread.Sleep(1000);
    }
}