using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityExample.DomainClasses;

namespace IdentityExample.ServiceLayer
{
    public interface ISubItemService : IEntityService<SubItem>
    {
        SubItem GetById(int id);
    }
}
