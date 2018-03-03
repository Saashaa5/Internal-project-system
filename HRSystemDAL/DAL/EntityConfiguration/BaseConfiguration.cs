using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSystemDAL.DAL;

namespace DAL.DAL.EntityConfiguration
{
    public class BaseConfiguration<T> : EntityTypeConfiguration<T> where T:Base //шаблонный базовый класс конфигурации 
    {
        public BaseConfiguration():base()
        {
            HasKey(x => x.ID);
        }
    }
}
