using System.Threading.Tasks;
using Xunit;

namespace OngProject.Test
{
    public class AuthControllerTest : TestScenarioBase
    {
        [Fact]
        public async Task ValidLoginAdminTest()
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
                var value = await response.Content.ReadAsStringAsync();

                //Assert
                response.EnsureSuccessStatusCode();
            }
        }
    }
}