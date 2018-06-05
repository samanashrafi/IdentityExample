using IdentityExample.DomainClasses;
using IdentityExample.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityExample.ServiceLayer
{
    public interface ISubFreeContentService : IEntityService<SubFreeContent>
    {
        SubFreeContent GetById(int id);
    }
}
