using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSystemDAL.Repository;

namespace HRSystemDAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
       
        private readonly Dictionary<Type, object> repos = new Dictionary<Type, object>();
        private readonly Object contextLock = new Object();
        private readonly ProjectsDbContext context;

        public UnitOfWork(ProjectsDbContext projectDbContext)
        {
            context = projectDbContext;
        }
        public GenericRepository<T> GetRepository<T>() where T : class
        {
            //Если репозиторий не создан
            if (!repos.ContainsKey(typeof(T)))
            {
                //Создаем
                repos.Add(typeof(T), new GenericRepository<T>(context));
            }
            //иначе вернуть существуйющий репозиторий
            return repos[typeof(T)] as GenericRepository<T>;
        }

        public void Save()
        {
            lock (contextLock)  //lock для сохранения данных в бд
            {
                context.SaveChanges();
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
