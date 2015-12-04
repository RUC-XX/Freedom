using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using System.Web;
using WebMatrix.WebData;

namespace Freedom
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembership : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<FreedomMembershipContext>(null);

                try
                {
                    using (var context = new FreedomMembershipContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }
                    WebSecurity.InitializeDatabaseConnection("FreedomConnectionString", "Userprofile", "UserID", "UserName", autoCreateTables: true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class FreedomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly bool _authorize;

        private bool _isPermissionFail = false;

        public FreedomAuthorizeAttribute()
        {
            if (HttpContext.Current.User.Identity.Name != "")
            {
                _authorize = true;
            }
            else
            {
                _authorize = false;
            }
        }

        public FreedomAuthorizeAttribute(string permission)
        {
            if (HttpContext.Current.User.Identity.Name != "")
            {
                _authorize = PermissionManager.CheckUserHasPermision(HttpContext.Current.User.Identity.Name, permission);
                if (_authorize == false)
                {
                    _isPermissionFail = true;
                }
            }
            else
            {
                _authorize = false;
            }
            //_authorize = true;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return _authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (_isPermissionFail)
            {
                filterContext.HttpContext.Response.Redirect("/Admin/PermissionError");
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            
        }
    }
}
