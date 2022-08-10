using System.Linq;
using ACYZenWebApp1.Controllers.BLZenAutomation.BasicOperation;
using ACYZenWebApp1.Controllers.BLZenAutomation.Pages;
using ACYZenWebApp1.Controllers.Pages;
using ACYZenWebApp1.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ACYZenWebApp1.Controllers.BLZenAutomation;

public class MainOfBLZenAutomation : UiBase
{
    private static ResponseGetPhoneNomber _responseGetPhoneNumber;
    ACYZenWebAppContext db;
   /*private static string _firstName = "Юлия";
    private static string _surname = "Нестерова";
    private static string _login = "you-li-ne.ster-";
    private static string _channnelName = "Hовости";*/

    /*public static async Task AddNewChannelsToDb(DZenActionContext db, Channel channel, string _firstName, string _surname, string _preLogin, int _loginNuberPre, int _loginNuberPost)
    {
        (channel._chanelUrl, channel._login) = await Registration3NewAccount1Number(db, channel, _firstName, _surname, _preLogin, channel._channnelName,_loginNuberPre, _loginNuberPost);
            
        db.Channels.Add(channel);
        // сохраняем в бд все изменения
        db.SaveChanges();
    }*/

    public static async Task Registration3NewAccount1NumberAndAddNewChannelsToDb(int zenActionid,
        ACYZenWebAppContext db, /*Channel channel, */string _firstName, string _surname, string _login,
        string _channnelName, int _loginNuberPre, int _loginNuberPost)
    {
        //Получаем номер телефона //Для всех аккаунтов 1 номер
        string chanelUrl = null;
        string newLogin = null;
        Setup();
        _responseGetPhoneNumber = await GetSmsCodee.GetTelephoneNomber(Host, ApiGetPhoneNomber);
        long telNumber = _responseGetPhoneNumber.TelNomber;
        string idNum = _responseGetPhoneNumber.IdNum;
        for (int i = _loginNuberPre; i <= _loginNuberPost; i++)
        {
            if (!new RegistrationPage(Driver, Wait).Open().InputLoginAndPassword(_firstName, i,
                    _surname, _login, Password, _loginNuberPost)) continue;
            Console.WriteLine($"Аккаунт {i}:");
            newLogin = _login + i;
            await BasicOperation.BasicOperation.InputAndConfirmTelNumber(Driver, Wait, telNumber, idNum);
            await GetSmsCodee.TelNomberStatusSend(Host, ApiKey, idNum);
            await BasicOperation.BasicOperation.GetAndInputSmsCode(Driver, Wait, idNum, i);
            await new AuthPage(Driver, Wait).Open().Auth(i, newLogin, Password, idNum, null, _loginNuberPost);
            //await BasicOperation.Auth(Driver, Wait, i, NewLogin, Password, idNum, null, a1);
            new ZenStudio(Driver, Wait).Open().SetChannelSettings(_channnelName);
            new SecurityQuestion(Driver, Wait).Open().SetKv(KvAnswer);
            await new AccountPhoneNumbers(Driver, Wait).Open().DeleteTelNomber(Password, idNum, newLogin);
            new ZenStudio(Driver, Wait).Open();
            string channellUrl = new ChannelPage(Driver, Wait).GotoChanelAndGetUrl();
            //.GetChannelMetaTag();
            //channel.Login = newLogin;
            Quit(Driver, Wait);
            if (!db.Channels.Any(c=>c.Login==newLogin))
            {
                db.Channels.AddRange(
                    new Channel
                    {
                        Login = newLogin, ChannnelName = _channnelName, ChanelUrl = channellUrl,
                        ZenActionId = zenActionid
                    }
                );
                db.SaveChanges();
            }
            /*db.Channels.Add(channel);
            // сохраняем в бд все изменения
            db.SaveChanges();*/
        }
        Driver.Close();
        Driver.Quit();
    }
}