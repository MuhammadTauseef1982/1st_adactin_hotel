using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Text.RegularExpressions;
namespace _1st_adactin_hotel
{
    public class Tests
    {
        //https://www.youtube.com/playlist?list=PLyR3u3h9srduMQ0G2SefuMz3Dkkkzs59k
        //Amir Imam
        /* if we write "Headless = true" we cannot see Test activity
         * we increase or decrease the "SlowMo=50" value to speedup or speeddown execution
         * we can give browser name in Channel="chrome" or Channel="msedge"
         * we do not require channel for Firefox or Webkit
          
          
         */

        [Test]
        public async Task Login_adactin01()
        {
            var playwright = await Playwright.CreateAsync();
            //var browser = await playwright.Firefox.LaunchAsync(
            //var browser = await playwright.Webkit.LaunchAsync(
            var browser = await playwright.Chromium.LaunchAsync(
                    new BrowserTypeLaunchOptions {Headless=false, Channel = "chrome", SlowMo =500, });
            //new BrowserTypeLaunchOptions {Headless=false, Channel="chrome", SlowMo=500, Timeout=50000, });
            //new BrowserTypeLaunchOptions {Headless=false, Channel="msedge", SlowMo=500, Timeout=50000, });
            //new BrowserTypeLaunchOptions {Headless=false, Channel="Chromium", SlowMo=500, Timeout=50000, });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync("http://adactinhotelapp.com/");
            await page.FillAsync("#username", "Tauseef12345");
            await page.FillAsync("#password", "xyz123");
            await page.ClickAsync("#login");
            //ASSERTIONS 
            /* this works in PageTest
             await Expect(page.Locator("#app")).ToContainTextAsync("Welcome to Adactin Group of Hotels");
            
            /*
            var isExists = await page.Locator(selector: "text='Welcome to Adactin Group of Hotels'").IsVisibleAsync();
            Assert.IsTrue(isExists)
            IsVisibleAsync = not have a waiting mechanism
            */

            /*this works in PageTest
            await Expect(page.GetByText("Welcome to Adactin Group of Hotels")).ToBeVisibleAsync();
            ToBeVisibleAsync = have a waiting mechanism
            */
            Assert.AreEqual("http://adactinhotelapp.com/SearchHotel.php",page.Url);
            var locator = page.Locator(".welcome_menu").First;
            await Assertions.Expect(locator).ToHaveTextAsync(
                new Regex("Welcome to Adactin Group of Hotels"));
            await page.CloseAsync();
        }
        [Test]
        public async Task Login_adactin02()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions { Headless = false, SlowMo = 50, Timeout = 50000, });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync("http://adactinhotelapp.com/");
            await page.Locator("#username").FillAsync("Tauseef12345");
            await page.Locator("#password").FillAsync("xyz123");
            await page.Locator("#login").ClickAsync();

            var locator = page.Locator(".welcome_menu").First;
            await Assertions.Expect(locator).ToHaveTextAsync(
                new Regex("Welcome to Adactin Group of Hotels"));
            await page.CloseAsync();

        }
        [Test]
        public async Task Login_Video_adactin03()
        {
            // THIS TASK IS FOR VIDEO CREATION
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions { Headless = false, SlowMo = 50, Timeout = 50000, });
            var context = await browser.NewContextAsync(new()
                {
                    RecordVideoDir = "video/",
                    RecordVideoSize = new RecordVideoSize{Width=1366, Height=728,},
                    ViewportSize = new ViewportSize { Width = 1366, Height = 728, }

            });
            var page = await context.NewPageAsync();

            await page.GotoAsync("http://adactinhotelapp.com/");
            await page.Locator("#username").FillAsync("Tauseef12345");
            await page.Locator("#password").FillAsync("xyz123");
            await page.Locator("#login").ClickAsync();
            
            var locator = page.Locator(".welcome_menu").First;
            await Assertions.Expect(locator).ToHaveTextAsync(
                new Regex("Welcome to Adactin Group of Hotels"));
            await page.PauseAsync();
            await page.CloseAsync();

        }
        [Test]
        public async Task Login_Tracing_adactin04()
        {
            // THIS TASK IS FOR VIDEO CREATION & TRACING/SNAPSHOT
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions { Headless = false, SlowMo = 50, Timeout = 50000, });
            var context = await browser.NewContextAsync();
            await context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
            var page = await context.NewPageAsync();
            await page.SetViewportSizeAsync(1366, 728);
            await page.GotoAsync("http://adactinhotelapp.com/");
            await page.FillAsync(selector: "#username", value: "Tauseef12345");
            await page.Locator("#username").FillAsync("Tauseef12345");
            await page.Locator("#password").FillAsync("xyz123");
            await page.Locator("#login").ClickAsync();
            //we also create screenshots in net 6.0 folder
            await page.ScreenshotAsync(new PageScreenshotOptions
            { 
                Path="LoginPage.jpg"
            });

            var locator = page.Locator(".welcome_menu").First;
            await Assertions.Expect(locator).ToHaveTextAsync(
                new Regex("Welcome to Adactin Group of Hotels"));
            
            await page.CloseAsync();

            await context.Tracing.StopAsync(new()
            {
                Path="Trace/trace.zip"
            });
            await context.CloseAsync();
            await browser.CloseAsync();
        }

        [Test]
        public async Task Login_Video_Tracing_adactin05()
        {
            // THIS TASK IS FOR VIDEO CREATION & TRACING BOTH
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions { Headless = false, SlowMo = 50, Timeout = 50000, });
            var context = await browser.NewContextAsync(new()
            {
                RecordVideoDir = "video/recording_"+DateAndTime.Now.ToString("yyyyMMdd_hhmmss").ToString(),
                RecordVideoSize = new RecordVideoSize { Width = 1366, Height = 728, },
                ViewportSize = new ViewportSize { Width = 1366, Height = 728, }

            });
            await context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
            var page = await context.NewPageAsync();
            await page.SetViewportSizeAsync(1366, 728);
            await page.GotoAsync("http://adactinhotelapp.com/");
            await page.Locator("#username").FillAsync("Tauseef12345");
            await page.Locator("#password").FillAsync("xyz123");
            await page.Locator("#login").ClickAsync();

            var locator = page.Locator(".welcome_menu").First;
            await Assertions.Expect(locator).ToHaveTextAsync(
                new Regex("Welcome to Adactin Group of Hotels"));
            
            await page.CloseAsync();
            await context.Tracing.StopAsync(new()
            {
                Path = "Trace/trace_" + DateAndTime.Now.ToString("yyyyMMdd_hhmmss")+".zip"
                //Path = "Trace/trace_" + DateAndTime.Now.ToString("dd_mm_yyyy_hh_mm_ss").ToString()+".zip"
            });
            await context.CloseAsync();
            await browser.CloseAsync();

        }
        [Test]
        public async Task testingMethod()
        {
            var playwright = await Playwright.CreateAsync();
       
            var browser = await playwright.Chromium.LaunchAsync(
                    new BrowserTypeLaunchOptions { Headless = false, Channel = "msedge", SlowMo = 50, Timeout = 50000, });
            var context = await browser.NewContextAsync(new() 
            { 
                RecordVideoDir = "video/orangehrm_"+DateAndTime.Now.ToString("ddMMyyyy_hhmmss").ToString(),
                RecordVideoSize = new RecordVideoSize { Width = 1366, Height=728,},
                ViewportSize = new ViewportSize { Width=1366, Height=728,}
                        
            });
            
            var Page = await context.NewPageAsync();
           
            await context.Tracing.StartAsync(new()
            {
                Screenshots=true,
                Snapshots=true,
                Sources=true
            });
            await Page.SetViewportSizeAsync(1366, 728);
            await Page.GotoAsync("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
         
            await Page.FillAsync("#app > div.orangehrm-login-layout > div > div.orangehrm-login-container > div > div.orangehrm-login-slot > div.orangehrm-login-form > form > div:nth-child(2) > div > div:nth-child(2) > input", "Admin");
            await Page.FillAsync("#app > div.orangehrm-login-layout > div > div.orangehrm-login-container > div > div.orangehrm-login-slot > div.orangehrm-login-form > form > div:nth-child(3) > div > div:nth-child(2) > input", "admin123");
            await Page.ClickAsync("#app > div.orangehrm-login-layout > div > div.orangehrm-login-container > div > div.orangehrm-login-slot > div.orangehrm-login-form > form > div.oxd-form-actions.orangehrm-login-action > button");
            Thread.Sleep(5000);
            await Page.ScreenshotAsync(new()
            { 
                Path = "afterlogin.png"
            });
            await context.Tracing.StopAsync(new()
            {
                Path= @"Trace\trace_orange_"+DateAndTime.Now.ToString("ddMMyyyy_hhmmss")+".zip"
            });
            await Page.CloseAsync();
            await context.CloseAsync();
            await browser.CloseAsync();

        }
    }
}