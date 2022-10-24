using ACYZenWebApp1.Controllers.BLZenAutomation.BasicOperation;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace ACYZenWebApp1.Controllers;

public class UiBase
{
    private static ChromeOptions _chromeOptions = new();
    public static IWebDriver Driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), _chromeOptions);
    public static WebDriverWait Wait =new(Driver, TimeSpan.FromSeconds(15));
    //public static IWebDriver driver = new ChromeDriver();
    protected const string Url = "https://zen.yandex.ru/";
    protected const string Password = "896008899gGsS!";
    protected const string NewPassword = "896008899gG!!";
    internal static string KvAnswer = "Шерлок";
    private static string _path = Directory.GetCurrentDirectory();
    protected internal static List<string> ListOfPhoto = new List<string>();
    protected static string PathToScreen = Directory
        .GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString())
        .ToString();
    internal static ResponseGetPhoneNomber ResponseGetPhoneNomber;
    protected internal static ResponseGetSmsCode ResponseGetSmsCode;
    private TelStatusGetNewSms _telStatusGetNewSms;
    protected internal const string ApiKey = "95fb1d66d8ca4c2fb81b8506df1516cd";
    protected internal const string Host = "https://vak-sms.com/api";
    private const string OperatorDefault = "None";
    private const string OperatorBeeline = "beeline";
    private const string OperatorLycamobile = "lycamobile";
    private const string OperatorMegafon = "megafon";
    private const string OperatorMts = "mts";
    private const string OperatorMTT = "mtt";
    private const string OperatorRostelecom = "rostelecom";
    private const string OperatorTele2 = "tele2";
    private const string OperatorYota = "yota";
    private const string Rent20Min = "false";
    private const string Rent8Hours = "true";
    protected internal const string ApiGetPhoneNomber = $"/getNumber/?apiKey={ApiKey}&service=ya&country=ru&operator={OperatorMTT}&rent={Rent8Hours}";
    [OneTimeSetUp]
    public static void Setup()
    {
        _chromeOptions = new ChromeOptions();
        // Отключить "Браузером управляет автоматизированное ПО"
        _chromeOptions.AddAdditionalOption("useAutomationExtension", false);
        _chromeOptions.AddExcludedArgument("enable-automation");
        if (!Directory.Exists(PathToScreen + $"/Dzen/AutoPostingDzen/PhotoFromPost/"))
            Directory.CreateDirectory(PathToScreen + $"/Dzen/AutoPostingDzen/PhotoFromPost/");
        if (!Directory.Exists(PathToScreen + $"/Dzen/AutoPostingDzen/Captcha/"))
            Directory.CreateDirectory(PathToScreen + $"/Dzen/AutoPostingDzen/Captcha/");
    }
    /*[SetUp]
    public void BeforeTest()
    {
        Driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), _chromeOptions);
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
    }*/
    public static void TearDown()
    {
        Driver.Close();
        Driver.Quit();
        DirectoryInfo folder = new DirectoryInfo(PathToScreen + $"/Dzen/AutoPostingDzen/PhotoFromPost/");
        foreach (FileInfo file in folder.GetFiles())
        {
            file.Delete();
        }
        DirectoryInfo foldercapcha = new DirectoryInfo(PathToScreen + $"/Dzen/AutoPostingDzen/Captcha/");
        foreach (FileInfo file in foldercapcha.GetFiles())
        {
            file.Delete();
        }
    }
    public static void Quit(IWebDriver Driver, WebDriverWait Wait)
    {
        //Driver.Navigate().GoToUrl(Url);
        //Driver.Manage().Window.Maximize();
        if (Driver.FindElements(By.XPath("//button[@aria-label='Меню профиля']"))
                .Count > 0)
        {
            Driver.FindElement(By.XPath("//button[@aria-label='Меню профиля']")).Click();
        }
        else if (Driver.FindElements(By.XPath("//button[@class='zen-ui-avatar _is-button _icon-size_32']"))
                     .Count > 0)
        {
            var menuProfil = Driver.FindElement(By.XPath("//button[@class='zen-ui-avatar _is-button _icon-size_32']"));
            menuProfil.Click();
        }
        else
        {
            Driver.Navigate().GoToUrl(Url);
            Driver.FindElement(By.XPath("//button[@aria-label='Меню профиля']")).Click();
        }
        Thread.Sleep(1000);
        if (Driver.FindElements(By.XPath("//div[contains(text(), 'Выйти')]"))
                .Count > 0)
        {
            Driver.FindElement(By.XPath("//div[contains(text(), 'Выйти')]")).Click();
        }
        else
        {
            var quit = Driver.FindElement(By.XPath("//span[contains(text(), 'Выйти')]"));
            quit.Click(); }
            
        Thread.Sleep(1000);
    }
}