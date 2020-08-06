using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDAngular.Binders
{
    public class AuthenticateAttribute : ActionFilterAttribute
    {

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        private void NotAuthorized(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Session.Clear();
            filterContext.Result = 
               new ObjectResult(filterContext.ModelState)
               {
                   StatusCode = StatusCodes.Status403Forbidden,
                   Value = "Not authorized access"
               };
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //Agora quem faz a validação é a api...
            //Então se o cookie existir é a primeira validação
            string token = ControllerHelper.GetToken(filterContext.HttpContext);


            if (string.IsNullOrWhiteSpace(token))
            {
                NotAuthorized(filterContext);
                return;
            }

        }
    }
}
