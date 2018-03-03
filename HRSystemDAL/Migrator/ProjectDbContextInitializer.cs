using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Migrations;
using HRSystemDAL;

namespace DAL.Migrator
{
    public class ProjectDbContextInitializer : MigrateDatabaseToLatestVersion<ProjectsDbContext, Configuration>
    {
      
    }

   
}
