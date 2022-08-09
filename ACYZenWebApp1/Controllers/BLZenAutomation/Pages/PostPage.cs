using System.Collections.ObjectModel;
using ACYZenWebApp1.Controllers.BLZenAutomation.AutoPostingDzen;
using ACYZenWebApp1.Controllers.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ACYZenWebApp1.Controllers.BLZenAutomation.Pages;

public class PostPage : Page
{
    private static IWebDriver _driver;
    private static WebDriverWait _wait;
    //private readonly string _url = UrlZen;
    
    /*public PostPage(WebDriver driver, string PostURL)
    {
        driver.Url = PostURL;
    }*/
    
    public PostPage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }
    
    public PostPage Open(string postUrl)
    {
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl(postUrl);
        return this;
    }
    public (string textText, string zagolovok) GetTextAndPhotoFromPostFromLink(WebDriverWait Wait)     
    {
        var zagolovok = _driver.FindElement(By.XPath("//h1[@class='article__title']"));
        string zagolovokText = zagolovok.Text;//GetAttribute("content");
        ReadOnlyCollection<IWebElement> paragraph = Wait.Until(e =>
            e.FindElements(By.XPath("//div[@class='article-render']//*[contains(@class, 'article-render')]")));
        var count = paragraph.Count;
        string textText = "";
        for (int i = 0; i < count; i++)
        {
            string text = paragraph[i].Text;
            textText += text + "\n";
        }
        //Делаем скриншот кртинок из поста и сохраняем их
        ReadOnlyCollection<IWebElement> photo =
            Wait.Until(e => e.FindElements(By.XPath("//div[@class='article-image-item__size']")));
        var photoCount = photo.Count;
        for (int i = 0; i < photoCount; i++)
        {
            Screenshot screenshotPhoto = ((ITakesScreenshot)photo[i]).GetScreenshot();
            screenshotPhoto.SaveAsFile(
                BasicOperationsForAutoPosting.PathToScreen + $"/Dzen/AutoPostingDzen/PhotoFromPost/screenshotPhoto+{i}.jpg",
                ScreenshotImageFormat.Png);
            string pathToScreen = BasicOperationsForAutoPosting.PathToScreen + $"/Dzen/AutoPostingDzen/PhotoFromPost/screenshotPhoto+{i}.jpg";
            UiBase.ListOfPhoto.Add(new string(pathToScreen));
        }
        return (textText, zagolovokText);
    }
}