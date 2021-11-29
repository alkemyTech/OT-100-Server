using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace OngProject.Test
{
    public class AuthControllerTest : TestScenarioBase
    {
        [Fact]
        public async Task ValidLoginTest()
        {
            //Arrange
            using (var server = CreateServer())
            {
                var request = new
                {
                    Url = "api/auth/login",
                    Body = new
                    {
                        email = "admin@localhost",
                        password = "Abc123."
                    }
                };

                //Act
                var response = await server.CreateClient().PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

                //Assert
                response.EnsureSuccessStatusCode();
            }
        }
        
        [Fact]
        public async Task InValidLoginTest()
        {
            //Arrange
            using (var server = CreateServer())
            {
                var request = new
                {
                    Url = "api/auth/login",
                    Body = new
                    {
                        email = "adminlocalhost",
                        password = "Abc123."
                    }
                };

                //Act
                var response = await server.CreateClient().PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

                //Assert
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
        
        
        
        [Fact]
        public async Task ValidRegisterTest()
        {
            //Arrange
            using (var server = CreateServer())
            {
                var request = new
                {
                    Url = "api/auth/register",
                    Body = new
                    {
                        email = $"{Guid.NewGuid().ToString()}@localhost",
                        password = "Abc123."
                    }
                };

                //Act
                var response = await server.CreateClient().PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

                //Assert
                response.EnsureSuccessStatusCode();
            }
        }
        
        [Fact]
        public async Task InValidRegisterPasswordShortTest()
        {
            //Arrange
            using (var server = CreateServer())
            {
                var request = new
                {
                    Url = "api/auth/register",
                    Body = new
                    {
                        email = $"{Guid.NewGuid().ToString()}@localhost",
                        password = "123"
                    }
                };

                //Act
                var response = await server.CreateClient().PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

                //Assert
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
        
        
        
        [Fact]
        public async Task ValidMeTest()
        {
            //Arrange
            using (var server = CreateServer())
            {
                var request = new
                {
                    Url = "api/auth/me",
                };
                
                var client = server.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxZmUyYmVmMi00NmEyLTRmODktYjg1NC1hZDUxNzRjMmZiN2UiLCJlbWFpbCI6ImFkbWluQGxvY2FsaG9zdCIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTYzNzgyMDg5NiwiZXhwIjoxNjM3OTA3Mjk2LCJpYXQiOjE2Mzc4MjA4OTZ9.LUjqaOt4uhD9G-d_mvvts36gomT_eq7QJcBblONmFg4");

                //Act
                var response = await client.GetAsync(request.Url);
                
                //Assert
                response.EnsureSuccessStatusCode();
            }
        }
        
        [Fact]
        public async Task InValidMeTest()
        {
            //Arrange
            using (var server = CreateServer())
            {
                var request = new
                {
                    Url = "api/auth/me",
                };
                
                var client = server.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    "NoTokenRey");

                //Act
                var response = await client.GetAsync(request.Url);
                
                //Assert
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }
        
    }
}