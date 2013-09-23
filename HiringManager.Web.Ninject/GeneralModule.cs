using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using HiringManager.Domain.EntityFramework;
using HiringManager.DomainServices;
using HiringManager.DomainServices.Impl;
using HiringManager.DomainServices.Transactions;
using HiringManager.Mappers;
using HiringManager.Transactions;
using HiringManager.Web.Ninject.Mappers;
using HiringManager.Web.Ninject.Transactions;
using Ninject.Modules;
using Ninject.Extensions.Conventions;
using Simple.Validation;

namespace HiringManager.Web.Ninject
{
    public class GeneralModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IClock>().To<Clock>();

            Kernel.Bind(configuration => configuration
                .FromAssemblyContaining<PositionService>()
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

            Kernel.Bind<IFluentTransactionBuilder>()
                .To<FluentTransactionBuilder>()
                ;

            Kernel.Bind<IFluentMapper>()
                .To<FluentMapper>()
                ;

            Kernel.Bind<IRepository>().To<Repository>();

            Kernel.Bind<IPrincipal>()
                .ToMethod(context => HttpContext.Current.User)
                ;
        }
    }
}
