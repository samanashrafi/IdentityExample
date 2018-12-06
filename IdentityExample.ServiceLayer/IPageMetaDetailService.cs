using IdentityExample.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityExample.ServiceLayer
{
    public interface IPageMetaDetailService : IEntityService<PageMetaDetail>
    {
        PageMetaDetail GetById(int id);
    }
}
