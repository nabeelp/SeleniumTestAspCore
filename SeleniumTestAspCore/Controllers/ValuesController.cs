using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace SeleniumTestAspCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                IWebDriver driver;
                ChromeOptions option = new ChromeOptions();
                option.AddArgument("--headless");
                option.AddArgument("--whitelisted-ips");
                option.AddArgument("--no-sandbox");
                option.AddArgument("--disable-extensions");
                //option.AddArgument("--disable-dev-shm-usage"); // overcome limited resource problems
                option.AddArgument("--disable-setuid-sandbox");
                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    driver = new RemoteWebDriver(option);
                }
                else
                {
                    driver = new ChromeDriver(@".\", option);
                }
                //var driver = new RemoteWebDriver(new Uri("http://selenium-hub:4444/wd/hub"), option);

                driver.Navigate().GoToUrl("http://www.google.com/ncr");

                Debug.WriteLine(driver.Title);
                string value1 = driver.Title;


                IWebElement query = driver.FindElement(By.Name("q"));

                query.SendKeys("TestingBot");

                query.Submit();

                Debug.WriteLine(driver.Title);
                string value2 = driver.Title;


                driver.Quit();

                return new string[] { value1, value2 };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
