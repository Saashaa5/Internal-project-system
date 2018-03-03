using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSystemDAL.UnitOfWork;

namespace HRSystemBLL.Services
{
   public class BaseService
    {

        protected IUnitOfWork UnitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

    }
}
