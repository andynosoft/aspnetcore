using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Alpaca.Tests {
    public class UnitTest1 {
        [Fact]
        public async Task Test1() {

            var hostBuilder = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Alpaca.Web.Startup>();

            using (var server = new TestServer(hostBuilder)) {

                HttpResponseMessage response;

                response = await server.CreateRequest("/api/protected").SendAsync("GET");

                Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);

                response = await server.CreateRequest("/api/token")
                    .And(
                        request => request.Content = new StringContent(
                            "username=pepe&password=pepe123",
                            System.Text.Encoding.UTF8,
                            "application/x-www-form-urlencoded"
                        )
                    )
                    .SendAsync("POST");

                Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

                var responseString = await response.Content.ReadAsStringAsync();
                var token = responseString.Split("\"".ToCharArray())[3];

                response = await server.CreateRequest("/api/protected")
                    .AddHeader("Authorization", "Bearer " + token)
                    .SendAsync("GET");

                Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            }

        }
    }
}
