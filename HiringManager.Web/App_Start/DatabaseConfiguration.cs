using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HiringManager.Domain.EntityFramework;

namespace HiringManager.Web.App_Start
{
    public static class DatabaseConfiguration
    {
        public static void Configure()
        {
#if DEBUG

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Repository>());

#endif
        }
    }
}