using ACYZenWebApp1.Controllers.BLZenAutomation.AutoPostingDzen.Prodazhki;
using ACYZenWebApp1.Controllers.BLZenAutomation.GettingPostFromSites.GettingPostFromAkket.AkketPages;
using ACYZenWebApp1.Controllers.BLZenAutomation.Pages;
using OpenQA.Selenium;

namespace ACYZenWebApp1.Controllers.BLZenAutomation.AutoPostingDzen;

public class BasicOperationsForAutoPosting : UiBase
{
    internal static string PathToScreen = Directory
        .GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString())
        .ToString();
    protected internal static (string postBodyTextRnd1, string zagolovokTextRnd) UniqueizerReplaceLetterOfGettingPost(string? postUrl, string? tema, int i)
    {
        if (postUrl == null)
        {
            (string postBody, string zagolovok) = new Akket(Driver, Wait).Open(tema).GetZagolovokAndTextBodyFromAkketPost(i);
            string postBodyTextRnd0 = postBody.Replace("р", "⁤р");
            string postBodyTextRnd1 = postBodyTextRnd0.Replace("к", "⁤к");
            string zagolovokTextRnd = zagolovok.Replace("к", "⁤к");
            return (postBodyTextRnd1, zagolovokTextRnd);
        }
        else
        {
            (string textBody, string textZagolovok) = new PostPage(Driver, Wait).Open(postUrl).GetTextAndPhotoFromPostFromLink(Wait);
             string zagolovokTextRnd1 = textZagolovok.Replace("к", "⁤к");
            string postBodyTextRnd1 = textBody.Replace("р", "⁤р");
            //string textTextRnd = textTextRnd0.Replace("а", "a");
            return (postBodyTextRnd1, zagolovokTextRnd1);
        }
    }
    protected internal (string postBodyTextRnd1, string zagolovokTextRnd) UniqueizerTranslationOfGettingPost(
        string? postUrl, string? tema, int i)
    {
        if (postUrl == null)
        {
            (string postBody, string zagolovok) = new Akket(Driver, Wait).Open(tema).GetZagolovokAndTextBodyFromAkketPost(i);
            string postBodyTextRnd0 = postBody.Replace("р", "⁤р");
            string postBodyTextRnd1 = postBodyTextRnd0.Replace("к", "⁤к");
            string zagolovokTextRnd = zagolovok.Replace("к", "⁤к");
            return (postBodyTextRnd1, zagolovokTextRnd);
        }
        else
        {
            (string textBody, string textZagolovok) = new PostPage(Driver, Wait).Open(postUrl).GetTextAndPhotoFromPostFromLink(Wait);
            string zagolovokTextRnd1 = textZagolovok.Replace("к", "⁤к");
            string postBodyTextRnd1 = textBody.Replace("р", "⁤р");
            //string textTextRnd = textTextRnd0.Replace("а", "a");
            return (postBodyTextRnd1, zagolovokTextRnd1);
        }
    }
    public static void AddPhotoInPost()
    {
        int count = new DirectoryInfo(PathToScreen + $"/Dzen/AutoPostingDzen/PhotoFromPost").GetFiles().Length;
        for (int i = 0; i < count; i++)
        {
            IWebElement addPhoto =
                Driver.FindElement(By.XPath("//button[@class='side-button side-button_logo_image']"));
            addPhoto.Click();
            var inputPhotoAdress = Driver.FindElement(By.XPath("//input[@class='image-popup__file-input']"));
            inputPhotoAdress.SendKeys(PathToScreen + $"/Dzen/AutoPostingDzen/PhotoFromPost/screenshotPhoto+{i}.jpg");
        }
    }
    protected internal static void AddProdazhka(string tema)
    {
        int count = new DirectoryInfo(PathToScreen + $"/Dzen/AutoPostingDzen/Prodazhki/{tema}").GetFiles().Length;
        for (int i = 1; i <= count; i++)
        {
            IWebElement addPhoto =
                Driver.FindElement(By.XPath("//button[@class='side-button side-button_logo_image']"));
            addPhoto.Click();
            var inputPhotoAdress = Driver.FindElement(By.XPath("//input[@class='image-popup__file-input']"));
            inputPhotoAdress.SendKeys(PathToScreen + $"/Dzen/AutoPostingDzen/Prodazhki/{tema}/{i}.jpg");
        }
        switch (tema)
        {
            case "Registrator" :
                AddLinkToSalesText(ProdazhkiText.ProdazhkiTextRegistrator, ProdazhkiText.LinkRegistrator);
                break;
            case "Mikardin" :
                AddLinkToSalesText(ProdazhkiText.ProdazhkiTextMikardin, ProdazhkiText.LinkMikardin);
                break;
            case "Antenna" :
                AddLinkToSalesText(ProdazhkiText.ProdazhkiTextAntenna, ProdazhkiText.LinkAntenna);
                break; 
            case "AntennaRussia" :
                AddLinkToSalesText(ProdazhkiText.ProdazhkiTextAntenna, ProdazhkiText.LinkAntenna);
                break;
            case "MikardinMagaziny" :
                AddLinkToSalesText(ProdazhkiText.ProdazhkiTextMikardin, ProdazhkiText.LinkMikardin);
                break;
        }
    }
    private static void AddLinkToSalesText(string TextProdazhki, string LinkProdazhki)
    {
        Driver.FindElement(
            By.XPath("//div[@class='zen-editor-block zen-editor-block-paragraph']")).SendKeys(TextProdazhki);
        var prodazhkaText =Driver.FindElement(
            By.XPath($"//div[@class='zen-editor-block zen-editor-block-paragraph']//*[contains(text(), '{TextProdazhki}')]"));
        prodazhkaText.Click();
        prodazhkaText.SendKeys(Keys.Home + Keys.Shift + Keys.End + Keys.ArrowDown);
        var addLink = Driver.FindElement(By.XPath("//div[@class='editor-toolbar__link-tools']"));
        addLink.Click();
        Driver.FindElement(By.XPath("//input[@placeholder='Введите ссылку']")).SendKeys(LinkProdazhki + "\n");
        var prodazhkaTextWithLink =Driver.FindElement(
            By.XPath($"//div[@class='zen-editor-block zen-editor-block-paragraph']//*[contains(text(), '{TextProdazhki}')]"));
        prodazhkaTextWithLink.Click();
        prodazhkaTextWithLink.SendKeys(Keys.Home + Keys.Shift + Keys.End + Keys.ArrowDown);
        Driver.FindElement(By.XPath("//div[@aria-label='Heading 2']")).Click();
    }
}