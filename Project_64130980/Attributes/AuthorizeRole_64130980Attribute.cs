using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class AuthorizeRole_64130980Attribute : AuthorizeAttribute
{
    private readonly string[] _allowedRoles; // Sử dụng tiền tố '_' để phân biệt

    public AuthorizeRole_64130980Attribute(params string[] roles)
    {
        _allowedRoles = roles;
    }

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        var role = httpContext.Session["Role"]?.ToString();
        return role != null && _allowedRoles.Contains(role);
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        var role = filterContext.HttpContext.Session["Role"]?.ToString();

        if (role != null && !_allowedRoles.Contains(role))
        {
            // Người dùng đã đăng nhập nhưng không có quyền
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary
                {
                    { "controller", "ThongKe_64130980" },
                    { "action", "Index" }
                });
        }
        else
        {
            // Người dùng chưa đăng nhập
            filterContext.Result = new RedirectResult("~/Account_64130980/Login");
        }
    }
}
