using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRUDAngular.Clients;
using CRUDAngular.ViewModels;
using System;
using System.Threading.Tasks;

namespace CRUDAngular.UnitTest
{
    [TestClass]
    public class LoginClient_UnitTest
    {
        [TestMethod]
        public async Task Login01()
        {
            //Arrange
            var login = new Login()
            {
                Username = "11234567890",
                Password = "09876543211"
            };
            var loginClient = new LoginClient();


            Console.WriteLine("Testing LoginClient.Login ");

            //Act
            try
            {
                var result = await loginClient.Login(login);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Username: " + login.Username);
                Console.WriteLine("Password: " + login.Password);
                Console.WriteLine("Error message: " + ex.Message);
                throw;
            }

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public async Task Login02()
        {
            //Arrange
            var login = new Login()
            {
                Username = "123",
                Password = "123"
            };
            var loginClient = new LoginClient();

            //Act
            try
            {
                var result = await loginClient.Login(login);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}
