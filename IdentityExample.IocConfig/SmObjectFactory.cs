using System;
using System.Web;
using System.Threading;
using StructureMap;
using StructureMap.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Data.Entity;
using System.Security.Claims;
using System.Security.Principal;
using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer;
using IdentityExample.ServiceLayer.Contracts;
using IdentityExample.DataLayer;


namespace IdentityExample.IocConfig
{
    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder =
            new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        private static Container defaultContainer()
        {
            return new Container(ioc =>
            {
                ioc.For<IDependencyResolver>()
                   .Singleton()
                   .Add<StructureMapSignalRDependencyResolver>();

                ioc.For<IIdentity>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(() => getIdentity());

                ioc.For<IUnitOfWork>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<ApplicationDbContext>();
                // Remove these 2 lines if you want to use a connection string named connectionString1, defined in the web.config file.
                //.Ctor<string>("connectionString")
                //.Is("Data Source=(local);Initial Catalog=TestDbIdentity;Integrated Security = true");

                ioc.For<ApplicationDbContext>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());
                ioc.For<DbContext>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());

                ioc.For<IUserStore<ApplicationUser, int>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<CustomUserStore>();

                ioc.For<IRoleStore<CustomRole, int>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<CustomRoleStore>();

                ioc.For<IAuthenticationManager>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(() => HttpContext.Current.GetOwinContext().Authentication);

                ioc.For<IApplicationSignInManager>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<ApplicationSignInManager>();

                ioc.For<IApplicationRoleManager>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<ApplicationRoleManager>();

                // map same interface to different concrete classes
                ioc.For<IIdentityMessageService>().Use<SmsService>();
                ioc.For<IIdentityMessageService>().Use<EmailService>();

                ioc.For<IApplicationUserManager>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<ApplicationUserManager>()
                   .Ctor<IIdentityMessageService>("smsService").Is<SmsService>()
                   .Ctor<IIdentityMessageService>("emailService").Is<EmailService>()
                   .Setter(userManager => userManager.SmsService).Is<SmsService>()
                   .Setter(userManager => userManager.EmailService).Is<EmailService>();

                ioc.For<ApplicationUserManager>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(context => (ApplicationUserManager)context.GetInstance<IApplicationUserManager>());

                ioc.For<ICustomRoleStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomRoleStore>();

                ioc.For<ICustomUserStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomUserStore>();

                //config.For<IDataProtectionProvider>().Use(()=> app.GetDataProtectionProvider()); // In Startup class

                ioc.For<IFreeContentService>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<FreeContentService>();

                ioc.For<ISubFreeContentService>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<SubFreeContentService>();

                ioc.For<IContactService>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<ContactService>();

                ioc.For<ISubItemService>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<SubItemService>();
            });
        }

        private static IIdentity getIdentity()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                return HttpContext.Current.User.Identity;
            }

            return ClaimsPrincipal.Current != null ? ClaimsPrincipal.Current.Identity : null;
        }
    }
}