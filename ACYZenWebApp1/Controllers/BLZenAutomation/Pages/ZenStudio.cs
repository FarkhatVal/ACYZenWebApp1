using System.Collections.ObjectModel;
using ACYZenWebApp1.Controllers.BLZenAutomation.AutoPostingDzen;
using ACYZenWebApp1.Controllers.BLZenAutomation.BasicOperation;
using ACYZenWebApp1.Controllers.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace ACYZenWebApp1.Controllers.BLZenAutomation.Pages;

public class ZenStudio : Page
{
    private static IWebDriver _driver;
    private static WebDriverWait _wait;
    private readonly string _url = UrlZen + Endpoints.ZenStudio;

    public ZenStudio(IWebDriver driver)
    {
        driver.Url = _url;
    }

    public ZenStudio(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public ZenStudio Open()
    {
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl(_url);
        return this;
    }

    public void SetChannelSettings(string channnelName)
    {
        var chanalNameSend = _wait.Until(d => d.FindElement(By.XPath(ZenStudioXPathSelectors.ChannelNameInputField)));
        chanalNameSend.SendKeys(channnelName);
        var chanalNameOk = _driver.FindElement(By.XPath(ZenStudioXPathSelectors.ChannelNameSave));
        chanalNameOk.Click();
        //Настройки
        //Thread.Sleep(3000);
        if (_driver.FindElements(
                    By.XPath(ZenStudioXPathSelectors.SetingButton1))
                .Count > 0)
        {
            _wait.Until(d => d.FindElement(By.XPath(ZenStudioXPathSelectors.SetingButton1))).Click();
        }
        else
        {
            var settings = _wait.Until(d => d.FindElement(By.XPath(ZenStudioXPathSelectors.SetingButton2)));
            settings.Click();
        }

        ChannelSettings();
    }

    public static void ChannelSettings()
    {
        if (_driver.FindElements(By.XPath("//input[@type='checkbox']")).Count > 1)
        {
            if (_driver.FindElement(By.XPath("(//input[@type='checkbox'])[2]"))
                    .GetAttribute("aria-checked") == "false")
            {
                var settings1 = _wait.Until(d => d.FindElement(By.XPath("(//input[@type='checkbox'])[2]")));
                settings1.Click();
            }
        }
        else if (_wait.Until(d =>
                     d.FindElement(By.XPath("//input[@type='checkbox']")).GetAttribute("aria-checked") == "false"))
        {
            var settings1 = _wait.Until(d => d.FindElement(By.XPath("//input[@type='checkbox']")));
            settings1.Click();
        }

        var settings2 = _wait.Until(d => d.FindElement(By.XPath("//button[@type='submit']")));
        //if (_driver.FindElements(By.XPath("//input[@aria-checked='false']")).Count > 0) settings1.Click();
        Thread.Sleep(1000);
        if (settings2.Enabled) settings2.Click();
    }

    /*public ZenStudio GotoChaneAndGetlUrl(string Login, int i)
    {
        /*var chanel = _wait.Until( d => d.FindElement(By.XPath(ZenStudioXPathSelectors.GoToChannelButton)));
        chanel.Click();#1#
        string chanelUrl = _wait.Until(d => d.FindElement(By.XPath("//div[@class='header__header-1C']/a")))
            .GetAttribute("href");
        //string chanelUrl =  _driver.Url;
        //string chanelUrl = $"https://zen.yandex.ru/id{chanel}";
        Console.WriteLine($"\tссылка на канал: {chanelUrl}");
        _driver.Navigate().GoToUrl(chanelUrl);
        return this;
    }
    public void GetChannelMetaTag()
    {
        var allOrNon = _driver.FindElement(By.XPath("//meta[@property='robots']"));
        string allOrNonText = allOrNon.GetAttribute("content");
        Console.WriteLine($"\tСтатус канала: {allOrNonText}");
    }*/

    public void CreateNewPost(string? service, string zagolovok, string postBody, string tema)
    {

        _driver.FindElement(By.XPath(ZenStudioXPathSelectors.Add)).Click();
        _driver.FindElement(By.XPath(ZenStudioXPathSelectors.CreatePost)).Click();
        Thread.Sleep(2000);
        if (_driver.FindElements(By.XPath(ZenStudioXPathSelectors.PopUp)).Count > 0)
        {
            var target = _wait.Until(d => d.FindElement(By.XPath(ZenStudioXPathSelectors.PopUp)));
            Actions builder = new Actions(_driver);
            builder.MoveToElement(target, -375, -436).Click().Build().Perform();
        }

        switch (service)
        {
            case "Akket.ru":
                BasicOperationsForAutoPosting.AddPhotoInPost();
                AddTextBody(zagolovok, postBody);
                //DeleteEmptyLinesInNewPost();
                Thread.Sleep(500);
                BasicOperationsForAutoPosting.AddProdazhka(tema);
                break;
            case "Yandex.ru":
                AddTextBody(zagolovok, postBody);
                // DeletEmptyLinesInNewPost();
                BasicOperationsForAutoPosting.AddPhotoInPost();
                BasicOperationsForAutoPosting.AddProdazhka(tema);
                break;
        }

        Thread.Sleep(10000);
    }

    public void AddTextBody(string zagolovok, string postBody)
    {
        var zagolovokInput = _wait.Until(d => d.FindElement(By.XPath(ZenStudioXPathSelectors.Zagolovok)));
        zagolovokInput.SendKeys(zagolovok);
        var textBody = _wait.Until(d => d.FindElement(By.XPath(ZenStudioXPathSelectors.TextBody)));
        textBody.SendKeys(postBody + "\n");
    }


    private static void DeleteEmptyLinesInNewPost()
    {
        ReadOnlyCollection<IWebElement> paragraph = _wait.Until(e =>
            e.FindElements(By.XPath("//div[@class='public-DraftStyleDefault-block public-DraftStyleDefault-ltr']")));
        var count = paragraph.Count;
        //string textText = "";
        for (int i = 0; i < count; i++)
        {
            if (_driver.FindElements(
                        By.XPath("//div[@class='public-DraftStyleDefault-block public-DraftStyleDefault-ltr']"))[i]
                    .GetAttribute("data-text") == null)
            {
                _wait.Until(e =>
                        e.FindElements(
                            By.XPath("//div[@class='public-DraftStyleDefault-block public-DraftStyleDefault-ltr']")))[i]
                    .SendKeys("\b");
            }
        }

    }

    public static async Task PublishPost(string Login)
    {
        Thread.Sleep(3000);
        _wait.Until(d => d.FindElement(By.XPath(ZenStudioXPathSelectors.Opublikovat))).Click();
        Thread.Sleep(1000);
        if (_driver.FindElements(By.XPath(ZenStudioXPathSelectors.SettingsWindowOpened)).Count > 0)
        {
            ChannelSettings();
        }

        if (_driver.FindElements(By.XPath(ZenStudioXPathSelectors.SettingsWindowOpened)).Count > 0)
        {
            _driver.FindElement(By.XPath(ZenStudioXPathSelectors.CloseSettingsWindowButton)).Click();
            _driver.FindElement(By.XPath(ZenStudioXPathSelectors.Opublikovat)).Click();
        }

        _driver.FindElement(By.XPath(ZenStudioXPathSelectors.PicSelect)).Click();
        _driver.FindElement(By.XPath(ZenStudioXPathSelectors.OpublikovatOk)).Click();
        Thread.Sleep(2000);
        if (_driver.FindElements(By.XPath("//img[@class='captcha__image']")).Count > 0)
        {
            await AntiCaptcha.InputCaptchaCode("//img[@class='captcha__image']");
            Thread.Sleep(5000);
            _driver.FindElement(By.XPath(ZenStudioXPathSelectors.OpublikovatOk)).Click();
        }

        Thread.Sleep(1000);
        if (_driver.FindElements(By.XPath("//h1[@class='article__title']")).Count > 0) Console.WriteLine($"\t{Login}");
    }

    public static void GetStatOfChannel()
    {
        _driver.FindElement(By.XPath("//span[@class][contains(text(), 'Статистика')]")).Click();
        Thread.Sleep(1000);
        if (_driver.FindElements(
                    By.XPath(
                        "//span[@class='help__link help__link_placeholder'][contains(text(), 'Карта дочитываний')]"))
                .Count > 0)
        {
            var target = _wait.Until(d =>
                d.FindElement(By.XPath(
                    "//span[@class='help__link help__link_placeholder'][contains(text(), 'Карта дочитываний')]")));
            Actions builder = new Actions(_driver);
            builder.MoveToElement(target, -375, -436).Click().Build().Perform();
        }

        string subscribers = _driver
            .FindElement(By.XPath("//span[@class='Text Text_weight_medium Text_typography_headline-22-26']")).Text;
        Console.WriteLine($"\t{subscribers}");
        _driver.FindElement(By.XPath("//li[@aria-selected][contains(text(), 'Публикации')]")).Click();
        string postsCount = _wait
            .Until(d => d.FindElement(By.XPath("(//span[@class='statistics-publications__text'])[2]"))).Text;
        Console.WriteLine($"\tвсего: {postsCount}");
        string feedShowsCount =
            _wait.Until(d => d.FindElement(By.XPath("(//td[@data-column-id='feedShows'])[1]"))).Text;
        Console.WriteLine($"\t\tвсего показов: {feedShowsCount}");
        string stat = _driver.FindElement(By.XPath("(//span[@class='statistics-publications__bold'])[1]")).Text;
        Console.WriteLine($"\t\tвсего дочитываний: {stat}");
        string readTime = _driver.FindElement(By.XPath("(//td[@data-column-id='sumViewTimeSec'])[1]")).Text;
        Console.WriteLine($"\t\tвремя дочитываний: {readTime}");
        string likeCount = _driver.FindElement(By.XPath("(//td[@data-column-id='likes'])[1]")).Text;
        Console.WriteLine($"\t\tвсего лайков: {likeCount}");
        string commentsCount = _driver.FindElement(By.XPath("(//td[@data-column-id='comments'])[1]")).Text;
        Console.WriteLine($"\t\tвсего комментариев: {commentsCount}");
    }
}
