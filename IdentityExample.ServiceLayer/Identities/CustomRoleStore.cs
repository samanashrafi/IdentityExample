using System.Data.Entity;
using IdentityExample.DataLayer;
using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer.Contracts;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityExample.ServiceLayer
{
    public class CustomRoleStore :
        RoleStore<CustomRole, int, CustomUserRole>,
        ICustomRoleStore
    {
        private readonly IUnitOfWork _context;

        public CustomRoleStore(IUnitOfWork context)
            : base((DbContext)context)
        {
            _context = context;
        }
    }
}