using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSystemDAL.Repository;

namespace HRSystemDAL.UnitOfWork
{
    public interface IUnitOfWork 
    {
        GenericRepository<T> GetRepository<T>() where T : class;
        void Save();
    }
}
