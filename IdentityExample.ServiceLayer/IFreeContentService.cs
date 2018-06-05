using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityExample.ServiceLayer
{
    public interface IFreeContentService : IEntityService<FreeContent>
    {
        FreeContent GetById(int id);
    }
}
