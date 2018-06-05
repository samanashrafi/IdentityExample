using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityExample.ServiceLayer
{
    public interface IEntityService<T> : IService
        where T : class
    {
        void Create(T entity);
        void Delete(int id);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}
