namespace ACYZenWebApp1.Controllers.BLZenAutomation;

public class Opening : UiBase
{
    
    public static void OpeningPage()
    {
        Driver.Navigate().GoToUrl("https://passport.yandex.ru/registration");
        //new RegistrationPage(Driver, Wait).Open();
    }
    
}