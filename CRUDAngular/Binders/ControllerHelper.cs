using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDAngular.Binders
{
    public static class ControllerHelper
    {
        private const  string TOKEN_COOKIE_NAME = "CRUD_TOKEN";

        public static string TOKEN_COOKIE_VALUE = "xjMT6SPsfe23XiavIfNT6n89obSi1yHYC97O3VP8VE0aIaDTA9DQcX5JP6rDDqPN48fZFdjyQEOAQzJP";

        public static string GetController(Microsoft.AspNetCore.Routing.RouteData route)
        {
            if (route == null)
            {
                return "";
            }

            return route.Values["controller"].ToString();
        }

        public static string GetController(this ControllerBase controller)
        {
            return GetController(controller.RouteData);
        }

        public static string GetAction(Microsoft.AspNetCore.Routing.RouteData route)
        {
            if (route == null)
            {
                return "";
            }

            return route.Values["action"].ToString();
        }

        public static string GetToken(this ControllerBase controller)
        {
            return GetToken(controller.HttpContext);
        }

        public static string GetToken(HttpContext httpContext)
        {
            return httpContext.Request.Cookies[TOKEN_COOKIE_NAME];
        }

        public static void SetToken(this ControllerBase controller)
        {
            SetToken(controller.HttpContext, TOKEN_COOKIE_VALUE);
        }
        public static void SetToken(HttpContext httpContext, string token)
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddMinutes(120);
            cookieOptions.HttpOnly = false;

            httpContext.Response.Cookies.Append(TOKEN_COOKIE_NAME, token, cookieOptions);
        }

        public static void ClearToken(this ControllerBase controller)
        {
            ClearToken(controller.HttpContext);
        }

        private static void ClearToken(HttpContext httpContext)
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddMinutes(1);
            cookieOptions.HttpOnly = false;

            httpContext.Response.Cookies.Append(TOKEN_COOKIE_NAME, "", cookieOptions);
        }

        public static void ValidateToken(this ControllerBase controller)
        {
            ValidateToken(controller.HttpContext);
        }
        public static void ValidateToken(HttpContext httpContext)
        {
            if (TOKEN_COOKIE_NAME != GetToken(httpContext))
            {
                ClearToken(httpContext);
                throw new ApplicationException("Not authenticated");
            }
        }

    }
}
