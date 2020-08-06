using Newtonsoft.Json;
using CRUDAngular.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CRUDAngular.Clients
{
    public class LoginClient
    { 
        /// <summary>
        /// Fake Login to authentication server
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<string> Login(Login login)
        {
            if (login == null ||
                string.IsNullOrEmpty(login.Username) ||
                string.IsNullOrEmpty(login.Password) ||
                login.Username == "123" || 
                login.Password == "123"
                )
            {
                throw new ApplicationException("Invalid login.");
            }


            return "success";
        }
    }
}
