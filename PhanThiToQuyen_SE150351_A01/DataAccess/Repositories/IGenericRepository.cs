using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        void Insert(T obj);
        void Update(T obj);
        IEnumerable<T> GetAll();
        void Delete(object id);
        T GetById(object id);
    }
}
