using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace OngProject.Test
{
    public class TestScenarioBase
    {
        public HttpClient Client { get; private set; }

        public TestServer CreateServer()
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder();
            webHostBuilder.UseStartup<Startup>();
            var testServer = new TestServer(webHostBuilder);
            return testServer;
        }
    }
}