using NUnit.Framework;
using OpenQA.Selenium;
using TwoCaptcha.Captcha;

namespace ACYZenWebApp1.Controllers.BLZenAutomation.BasicOperation;

public abstract class AntiCaptcha : UiBase
    {
        public static async Task<string> GetCapthaCode(string XPathSelectorToCapcha)
        {
            Thread.Sleep(2000);
            var photo =
                        Wait.Until(e => e.FindElement(By.XPath(XPathSelectorToCapcha)));
            Screenshot screenshotPhoto = ((ITakesScreenshot)photo).GetScreenshot();
            screenshotPhoto.SaveAsFile(
                        UiBase.PathToScreen + $"/Dzen/AutoPostingDzen/Captcha/captcha+.png",
                        ScreenshotImageFormat.Png); 
            string pathToScreen = UiBase.PathToScreen + $"/Dzen/AutoPostingDzen/Captcha/captcha+.png";
            ListOfPhoto.Add(new string(pathToScreen));
                    
                TwoCaptcha.TwoCaptcha solver = new TwoCaptcha.TwoCaptcha("b8ff7987bf8b6a56623e620e5379aaa0");
                Normal captcha = new Normal();
                captcha.SetFile(pathToScreen);
                captcha.SetMinLen(4);
                captcha.SetMaxLen(20);
                captcha.SetCaseSensitive(true);
                captcha.SetLang("ru");

                try
                {
                    await solver.Solve(captcha);
                    //Console.WriteLine("Captcha solved: " + captcha.Code);
                    return captcha.Code;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred: " + e.Message);
                    return null;
                }
        }
        
        public static async Task InputCaptchaCode(string XPathSelectorToCapcha)
        {
            Thread.Sleep(1000);
            var captchaCode = await GetCapthaCode(XPathSelectorToCapcha);
            if (Driver.FindElements(By.XPath("//input[@placeholder='Введите символы с картинки']")).Count > 0)
            {
                var inputcaptchaCode1 = Driver.FindElement(By.XPath("//input[@placeholder='Введите символы с картинки']"));
                inputcaptchaCode1.SendKeys(captchaCode);
            }
            else
            {
                var inputcaptchaCode = Driver.FindElement(By.XPath("//input[@data-t='field:input-captcha_answer']"));
                inputcaptchaCode.SendKeys(captchaCode);
                Thread.Sleep(5000);
                if (Driver.FindElements(
                            By.XPath("//button[@class='Button2 Button2_size_l Button2_view_action Button2_width_max']"))
                        .Count > 0)
                {
                    var captchaCodeOk = Driver.FindElement(By.XPath(
                        "//button[@class='Button2 Button2_size_l Button2_view_action Button2_width_max']"));
                    captchaCodeOk.Click();
                }
                Thread.Sleep(5000);
                if (Driver.FindElements(By.XPath("//div[@id='field:input-captcha_answer:hint']"))
                        .Count > 0)
                {
                    DirectoryInfo foldercapcha = new DirectoryInfo(PathToScreen + $"/Dzen/AutoPostingDzen/Captcha/");
                    foreach (FileInfo file in foldercapcha.GetFiles())
                    {
                        file.Delete();
                    
                    }
                    string captchaCode1 = await GetCapthaCode("//img[@class='captcha__image']");
                    var inputcaptchaCode1 = Driver.FindElement(By.XPath("//input[@data-t='field:input-captcha_answer']"));
                    inputcaptchaCode1.SendKeys("\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b");
                    inputcaptchaCode1.SendKeys(captchaCode1);
                    Thread.Sleep(5000);
                    var captchaCodeOk1 = Driver.FindElement(By.XPath("//button[@class='Button2 Button2_size_l Button2_view_action Button2_width_max']"));
                    captchaCodeOk1.Click();
                    if (Driver.FindElements(By.XPath("//div[@id='field:input-captcha_answer:hint']")).Count > 0)
                    {
                        Console.WriteLine("2 раза не распознана капча");
                    }
                }
            }

            if (Driver.FindElements(
                        By.XPath("//span[@class='zen-ui-button__content-wrapper'][contains(text(), 'Отправить')]"))
                    .Count > 0)
            {
                Driver.FindElement(
                    By.XPath("//span[@class='zen-ui-button__content-wrapper'][contains(text(), 'Отправить')]")).Click();
            }
        }

       
    }