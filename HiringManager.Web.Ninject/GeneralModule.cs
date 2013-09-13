using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HiringManager.Domain.EntityFramework;
using HiringManager.Domain.Mappers;
using HiringManager.DomainServices;
using HiringManager.Transactions;
using HiringManager.Web.ApplicationServices;
using Ninject.Modules;
using Ninject.Extensions.Conventions;

namespace HiringManager.Web.Ninject
{
    public class GeneralModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(configuration => configuration
                .FromAssemblyContaining<AccountService>()
                .SelectAllClasses()
                .BindAllInterfaces());

            Kernel.Bind(configuration => configuration
                .FromAssemblyContaining<HiringService>()
                .SelectAllClasses()
                .BindAllInterfaces()
                );

            Kernel.Bind(configuration => configuration
                .FromAssemblyContaining<CreatePositionRequestMapper>()
                .SelectAllClasses()
                .BindAllInterfaces()
                );

            Kernel.Bind(configuration => configuration
                .FromAssemblyContaining<CreatePosition>()
                .SelectAllClasses()
                .BindAllInterfaces()
                );

            Kernel.Bind<IRepository>().To<Repository>();
        }
    }
}
