using System.Collections.ObjectModel;
using ACYZenWebApp1.Controllers.BLZenAutomation.AutoPostingDzen;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ACYZenWebApp1.Controllers.BLZenAutomation.GettingPostFromSites.GettingPostFromAkket.AkketPages;

public class Akket : BLZenAutomation.GettingPostFromSites.GettingPostFromAkket.AkketPages.AkketPages
{
    private static IWebDriver _driver;
    private static WebDriverWait _wait;
    private readonly string _url = AkketMain;

    public Akket(IWebDriver driver)
    {
        driver.Url = _url;
    }

    public Akket(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public Akket Open(string tema)
    {
        _driver.Manage().Window.Maximize();
        switch (tema)
        {
            case "Registrator" :
                _driver.Navigate().GoToUrl(_url + AkketEndpoints.Gibdd);
                break;
            case "Antenna" :
                _driver.Navigate().GoToUrl(_url + AkketEndpoints.Operatory);
                break;
            case "AntennaRussia" :
                _driver.Navigate().GoToUrl(_url + AkketEndpoints.Russia);
                break;
            case "Mikardin" :
                _driver.Navigate().GoToUrl(_url + AkketEndpoints.Zhkh);
                break;
            case "MikardinMagaziny" :
                _driver.Navigate().GoToUrl(_url + AkketEndpoints.Magaziny);
                break;
        }
        return this;
    }

    public (string textText, string zagolovokText) GetZagolovokAndTextBodyFromAkketPost(int i)
    {
        ReadOnlyCollection<IWebElement> akketPost = _wait.Until(e =>
            e.FindElements(By.XPath("//div[@class='box']")));
        akketPost[i].Click();
            var zagolovok = _driver.FindElement(By.XPath("//h1[@class='title']"));
            string zagolovokText = zagolovok.Text;
            ReadOnlyCollection<IWebElement> paragraph = _wait.Until(e =>
                e.FindElements(By.XPath("//div[@class='entry']/p")));
            var count = paragraph.Count;
            string textText = "";
            for (int n = 0; n < count - 3; n++)
            {
                string text = paragraph[n].Text;
                textText += text + "\n";
            }

            var photo1 = _wait.Until(e =>
                e.FindElement(By.XPath("//img[@class='attachment-post-thumbnail size-post-thumbnail wp-post-image']")));
            var photoUrl = photo1.GetAttribute("src");
            _driver.Navigate().GoToUrl(photoUrl);
            var photo = _wait.Until(e => e.FindElement(By.XPath("//img")));
            Screenshot screenshotPhoto = ((ITakesScreenshot)photo).GetScreenshot();
            screenshotPhoto.SaveAsFile(
                BasicOperationsForAutoPosting.PathToScreen +
                $"/Dzen/AutoPostingDzen/PhotoFromPost/screenshotPhoto+0.jpg",
                ScreenshotImageFormat.Png);
            string pathToScreen = BasicOperationsForAutoPosting.PathToScreen +
                                  $"/Dzen/AutoPostingDzen/PhotoFromPost/screenshotPhoto+0.jpg";
            UiBase.ListOfPhoto.Add(new string(pathToScreen));
            return (textText, zagolovokText);
    }
}