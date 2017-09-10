using System.Collections.Generic;
using DrMturhan.Server.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DrMturhan.Server.Controllers.api
{
    public class AppUtils
    {
        internal static IActionResult SignIn(Kullanici user, IList<string> roles)
        {
            var userResult = new { User = new { DisplayName = user.UserName, Roles = roles } };
            return new ObjectResult(userResult);
        }
    }
}