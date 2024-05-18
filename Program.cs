using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

FirefoxDriver driver = null;

try
{
    // Make sure to put your thread urls inside urls.txt
    string fileName = @"urls.txt";

    string[] urls = File.ReadAllLines(fileName);

    var deviceDriver = FirefoxDriverService.CreateDefaultService();
    deviceDriver.HideCommandPromptWindow = false;
    FirefoxOptions options = new FirefoxOptions();
    options.AddArguments("--disable-infobars");

    driver = new FirefoxDriver(deviceDriver, options);
    //driver.Manage().Window.Maximize();

    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

    foreach (var url in urls)
    {

        driver.Navigate().GoToUrl(url);
        driver.Manage().Cookies.DeleteAllCookies();

        // Fill in your active browser cookies
        driver.Manage().Cookies.AddCookie(new Cookie("__cf_bm", "HERE"));
        driver.Manage().Cookies.AddCookie(new Cookie("xf_user", "HERE"));
        driver.Manage().Cookies.AddCookie(new Cookie("xf_tfa_trust_973969", "HERE"));
        driver.Manage().Cookies.AddCookie(new Cookie("xf_session", "HERE"));

        driver.Navigate().GoToUrl(url);

        Thread.Sleep(2000);

        try
        {
            var bumpButton = driver.FindElement(By.Id("sc_main_bump_button"));
            bumpButton.Click();

            Console.WriteLine($"Thread {url} has been bumped!\n");

            Thread.Sleep(1500);
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine($"{url} has been skipped!\n");
        }
    }

    Console.WriteLine($"All threads bumped! :)\n");

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.ReadLine();
}
finally
{
    driver.Quit();
    Environment.Exit(0);
}