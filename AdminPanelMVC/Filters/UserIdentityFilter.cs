using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdminPanelMVC.Filters
{
    public class UserIdentityFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userNameClaim = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

                if (!string.IsNullOrEmpty(userNameClaim))
                {
                    userNameClaim = CapitalizeFirstLetter(userNameClaim);
                }

                if (context.Controller is Controller controller)
                {
                    controller.ViewBag.UserName = userNameClaim;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Action tamamlandıktan sonra yapılacak işlemler
        }

        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.Substring(0, 1).ToUpper() + input.Substring(1);
        }
    }
}
