using IdentityExample.DataLayer;
using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer.Contracts;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityExample.ServiceLayer
{
    public class CustomUserStore :
        UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>,
        ICustomUserStore
    {
        private readonly IUnitOfWork _context;

        public CustomUserStore(IUnitOfWork context)
            : base((ApplicationDbContext)context)
        {
            _context = context;
        }

    }
}