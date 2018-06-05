using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using IdentityExample.DomainClasses;

namespace IdentityExample.ServiceLayer.Contracts
{
    public interface ICustomRoleStore : IDisposable
    {
        Task<CustomRole> FindByIdAsync(int roleId);
        Task<CustomRole> FindByNameAsync(string roleName);
        Task CreateAsync(CustomRole role);
        Task DeleteAsync(CustomRole role);
        Task UpdateAsync(CustomRole role);
        DbContext Context { get; }
        bool DisposeContext { get; set; }
        IQueryable<CustomRole> Roles { get; }
    }
}