using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HiringManager.EntityFramework;
using HiringManager.Web.Models;
using HiringManager.Web.Models.Accounts;

namespace HiringManager.Web.App_Start
{
    public static class DatabaseConfiguration
    {
        public static void Configure()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Repository, EntityFramework.Migrations.Configuration>());
            new Repository().Database.Initialize(force: false);
        }
    }
}