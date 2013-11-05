using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HiringManager.EntityFramework;
using HiringManager.Web.Models.Accounts;

namespace HiringManager.Web.App_Start
{
    public static class DatabaseConfiguration
    {
        public static void Configure()
        {
#if DEBUG

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Repository>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());

#endif
        }
    }
}