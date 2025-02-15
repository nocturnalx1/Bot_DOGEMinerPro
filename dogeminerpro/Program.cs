using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.DevTools.V130.Debugger;
using System.Text.RegularExpressions;
using System.Media;

partial class Program
{
    private static double MaxHour = 1;
    private static DateTime StartTime = DateTime.Now;
    private static TimeSpan TimerSpan = new TimeSpan();
    private static DateTime CurrentTime = new DateTime();
    private static int TimeOutSec = 1;
    private static bool isFirstTime = true;
    private static System.Media.SoundPlayer sp1 = new SoundPlayer(@"D:\Backup\.Net\c#\seleniumTestWeb\selenium\selenium\Media\A3.wav");
    private static System.Media.SoundPlayer Abriendo_pagina = new SoundPlayer(@"D:\Backup\.Net\c#\seleniumTestWeb\selenium\selenium\Media\Abriendo_pagina.wav");
    private static System.Media.SoundPlayer Bandera = new SoundPlayer(@"D:\Backup\.Net\c#\seleniumTestWeb\selenium\selenium\Media\Bandera.wav");
    private static System.Media.SoundPlayer ExiteUnError = new SoundPlayer(@"D:\Backup\.Net\c#\seleniumTestWeb\selenium\selenium\Media\ExiteUnError.wav");
    private static System.Media.SoundPlayer Exitosamente = new SoundPlayer(@"D:\Backup\.Net\c#\seleniumTestWeb\selenium\selenium\Media\Exitosamente.wav");
    private static System.Media.SoundPlayer Iniciando = new SoundPlayer(@"D:\Backup\.Net\c#\seleniumTestWeb\selenium\selenium\Media\Iniciando.wav");
    private static System.Media.SoundPlayer ManualMente = new SoundPlayer(@"D:\Backup\.Net\c#\seleniumTestWeb\selenium\selenium\Media\ManualMente.wav");

    private static string userLogin = "Usuario";
    private static string passLogin = "Usuario";
    private static string dogeAddress = "Usuario";


    static void Main(string[] args)
    {
        
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WindowWidth = 90;
        Console.WindowHeight = 36;

        ChromeOptions option = new ChromeOptions
        {

        };

        option.AddArgument("--no-sandbox");
        option.AddArgument("no-first-run");
        option.AddArgument("--disable-gpu");
        option.AddArgument("--log-level-3");
        option.AddArgument("--disable-dev-shm-usage");
        option.AddArgument("--disable-notifications");
        option.AddArgument("--ignore-certificate-erors");
        option.AddArgument("no-default-browser-check");
        option.AddArgument("--window-size=700,800");
        option.AddArgument("--disable-infobars");
        option.AddArgument("--start-maximized");
        option.AddArgument("--disable-blink-features");
        option.AddArgument("--disable-blink-features=AutomationControlled");

        option.AddAdditionalChromeOption("useAutomationExtension", false);


        IWebDriver driver = new ChromeDriver("C:\\chromeDriver\\chromedriver-win64\\");
        DateTime StartTime = DateTime.Now;
        Console.WriteLine("[x] Start Time: " + StartTime.ToString());
        Iniciando.Play();


        PrintHelp();
        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true - will intercept the key and won't show it
                char c = keyInfo.KeyChar;
                if (c == 'x' || c == 'X')
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("[x] Adios..");
                    driver.Quit();
                    break;
                }
                else if (c == 'r' || c == 'R')
                {
                    ManualMente.Play();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("[x] Iniciando Manualmente");
                    isFirstTime = true;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    StartProcess(driver);
                }
                else if (c == 'f' || c == 'F')
                {
                    Bandera.Play();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("[x] Quitando la bandera First Time");
                    isFirstTime = false;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else if (c == 'h' || c == 'H')
                {
                    PrintHelp();
                }
                else if (c == 'a' || c == 'A')
                {
                    Console.Clear();
                    int iCountSec = 0;

                    bool IsExit = false;
                    bool IsExitTimeOut = false;
                    while (!IsExit)
                    {
                        if (Console.KeyAvailable)
                        {
                            keyInfo = Console.ReadKey(true); // true - will intercept the key and won't show it
                            c = keyInfo.KeyChar;

                            if (c == 'x' || c == 'X')
                            {
                                IsExit = true;
                            }

                        }

                        IsExitTimeOut = false;

                        while (!IsExitTimeOut)
                        {
                            iCountSec++;
                            if (iCountSec >= TimeOutSec)
                            {
                                IsExitTimeOut = true;
                                iCountSec = 0;
                            }
                            Thread.Sleep(1000);

                            if (IsExitTimeOut == false)
                            {

                                if (Console.KeyAvailable)
                                {
                                    keyInfo = Console.ReadKey(true); // true - will intercept the key and won't show it
                                    c = keyInfo.KeyChar;

                                    if (c == 'x' || c == 'X')
                                    {
                                        IsExit = true;
                                        IsExitTimeOut = true;
                                        TimeOutSec = 1;
                                        var cursor = Console.GetCursorPosition();
                                        Console.SetCursorPosition(cursor.Left, cursor.Top + 1);
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        Console.WriteLine("[x] Saliendo...");

                                        PrintHelp();
                                    }
                                }
                                if (!IsExitTimeOut)
                                {
                                    if ((iCountSec % 1) == 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine("[x] Proxima Ejecucion -> " + TimeSpan.FromSeconds(TimeOutSec - iCountSec).ToString(@"dd\.hh\:mm\:ss"));
                                        var cursor = Console.GetCursorPosition();
                                        Console.SetCursorPosition(cursor.Left, cursor.Top - 1);
                                    }
                                }
                            }
                        }
                        if (!IsExit)
                        {
                            StartProcess(driver);
                        }

                    }

                }
            }
            Thread.Sleep(1000);
        }
    }
    static void StartProcess(IWebDriver driver)
    {
        var CurrentTime = DateTime.Now;
        var TimerSpan = (TimeSpan)(CurrentTime - StartTime);
        var DiffTimer = TimeSpan.FromHours(MaxHour - TimerSpan.TotalHours);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("[x] Max Tiempo para reclamar: " + MaxHour.ToString());
        Console.WriteLine("[x] Tiempo para reclamar: " + DiffTimer.ToString(@"dd\.hh\:mm\:ss"));

        if (DiffTimer.TotalHours >= MaxHour || isFirstTime == true)
        {
            sp1.Play();
            isFirstTime = false;
            TimeOutSec = 1;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Navigate().GoToUrl("https://dogeminerpro.com/");

            Console.WriteLine("[x] Url: " + driver.Url);
            driver.Manage().Cookies.DeleteAllCookies();
            //openLogin;
            IWebElement openLogin = WaitUntilElementExists(driver, By.Id("openLogin"));
            openLogin.Click();
            Console.WriteLine("[x] Open Login...");

            IWebElement email = WaitUntilElementExists(driver, By.Id("email"));
            IWebElement pass = WaitUntilElementExists(driver, By.Id("password"));

            email.SendKeys(userLogin);
            pass.SendKeys(passLogin);
            Console.WriteLine("[x] Email -> " + userLogin);

            //
            IWebElement formBtnLogin = WaitUntilElementExists(driver, By.CssSelector(".btn-form-login"));
            formBtnLogin.Click();

            Console.WriteLine("[x] Espera a recarga de pagina");
            Abriendo_pagina.Play();
            WaitForPageToLoad(driver);

            Console.WriteLine("[x] Se recargo la pagina");

            //close-share id
            IWebElement BtncloseShare = WaitUntilElementExists(driver, By.Id("close-share"));
            IWebElement txtAddress = WaitUntilElementExists(driver, By.Id("address"));
            //Doge = DBvsjtmruFJLymArCpkGxDcGaEf8ojXeLg

            BtncloseShare.Click();
            Thread.Sleep(1000);

            Console.WriteLine("[x] Iniciando el retiro");
            IWebElement btnWithdraw = WaitUntilElementExists(driver, By.Id("btn-withdraw"));
            btnWithdraw.Click();
            Thread.Sleep(1000);

            Console.WriteLine("[x] Doge Address -> " + dogeAddress);

            txtAddress.SendKeys(dogeAddress);
            Thread.Sleep(1000);

            IWebElement btnWithdrawContinue = WaitUntilElementVisible(driver, By.Id("btn-withdraw-continue"));
            btnWithdrawContinue.Click();
            Thread.Sleep(1000);

            IWebElement BtncontinueEithdraw = WaitUntilElementVisible(driver, By.Id("continue-withdraw"));
            BtncontinueEithdraw.Click();
            IWebElement SucessMsg = WaitUntilElementExists(driver, By.Id("alert-withdraw-success"), 25);
            IWebElement ErrorMsg = WaitUntilElementExists(driver, By.Id("alert-withdraw-error"), 25);
            //.alert-danger-box
            //.


            Console.WriteLine("[x] Esperando la respuesta....");
            if (WaitForTextIsNotEmpty(SucessMsg, 20))
            {
                Exitosamente.Play();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[OK] Success: " + SucessMsg.Text);
            }

            if (WaitForTextIsNotEmpty(ErrorMsg, 20))
            {
                ExiteUnError.Play();
                Console.ForegroundColor = ConsoleColor.Red;
                //Exceeded withdrawal limit per hour. Try again in 6 hours.
                var HourNumber = double.Parse(Regex.Match(ExtractNumber(ErrorMsg.Text), @"\d+").Value);
                MaxHour = HourNumber;
                Console.WriteLine("[ERR] ERROR -> " + ErrorMsg.Text);
                Console.WriteLine("[ERR] Intenta de nuevo en " + HourNumber.ToString() + " Horas");


            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            //sms-cancel id
            IWebElement BtnSmsCancel = WaitUntilElementExists(driver, By.Id("sms-cancel"), 25);
            IWebElement btnAccount = WaitUntilElementExists(driver, By.XPath("/html/body/header/div/div/div/nav/ul/li[8]/a"), 25);
            BtnSmsCancel.Click();

            Console.WriteLine("[x] Saliendo..");
            Thread.Sleep(1000);
            btnAccount.Click();
            Console.WriteLine("[x] OK!");
        }
        else
        {
            var rnd = new Random();

            TimeOutSec = (int)(rnd.Next(60, 1200));
            Console.WriteLine("[x] Falta : " + DiffTimer.ToString(@"dd\.hh\:mm\:ss") + " Horas para reclamar");
        }

    }

    static void PrintHelp()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[x] Para iniciar ayuda con la letra H");
        Console.WriteLine("[x] Para iniciar con la letra A (loop)");
        Console.WriteLine("[x] Para iniciar con la letra R (Manual)");
        Console.WriteLine("[x] Para quitar el flag FirstTime con la letra F");
        Console.WriteLine("[x] Para Salir con la letra X");
    }
    static string ExtractNumber(string inputString)
    {
        return string.Join(",", new string(inputString
              .Where(c => char.IsBetween(c, '0', '9') || c == '.' || c == '-' || char.IsWhiteSpace(c))
              .ToArray()).Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
    }
    static IWebElement WaitUntilElementClickable(IWebDriver Driver, By elementLocator, int timeout = 10)
    {
        try
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
            throw;
        }
    }
    static bool WaitForTextIsNotEmpty(IWebElement element, int timeout = 10)
    {
        bool ret = false;
        bool load = false;
        int currentTimeout = 0;
        while (!load)
        {
            try
            {
                Thread.Sleep(1000);
                if (!string.IsNullOrEmpty(element.Text))
                {
                    load = true;
                    ret = true;
                }
                else
                {
                    currentTimeout++;
                    load = false;
                    if (currentTimeout >= timeout)
                    {
                        load = true;
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        return ret;

    }
    static bool WaitForPageToLoad(IWebDriver _driver, Action? doing = null)
    {
        IWebElement oldPage = _driver.FindElement(By.TagName("html"));
        if (doing != null)
        {
            doing();
        }


        WebDriverWait wait = new WebDriverWait(_driver, _driver.Manage().Timeouts().ImplicitWait);
        try
        {
            wait.Until(driver => ExpectedConditions.StalenessOf(oldPage)(_driver) &&
                ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            return true;
        }
        catch (Exception pageLoadWaitError)
        {
            return false;
        }
    }
    static void ClickAndWaitForPageToLoad(IWebDriver Driver, By elementLocator, int timeout = 10)
    {
        try
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            var element = Driver.FindElement(elementLocator);
            element.Click();
            wait.Until(ExpectedConditions.StalenessOf(element));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
            throw;
        }
    }
    static IWebElement? WaitUntilElementVisible(IWebDriver Driver, By elementLocator, int timeout = 10)
    {
        try
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("[x] Element with locator: '" + elementLocator + "' was not found.");
            return null;
        }
        catch (WebDriverTimeoutException)
        {
            Console.WriteLine("[x] TimeOut -> " + elementLocator);
            return null;

        }
        catch (Exception e)
        {
            Console.WriteLine("[x] TimeOut -> " + elementLocator + " <-> " + e.Message);
            return null;
        }
    }
    static IWebElement WaitUntilElementExists(IWebDriver Driver, By elementLocator, int timeout = 10)
    {
        try
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(elementLocator));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
            throw;
        }
    }
};