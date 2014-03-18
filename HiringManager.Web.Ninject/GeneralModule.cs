using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using HiringManager.DomainServices;
using HiringManager.DomainServices.Impl;
using HiringManager.DomainServices.Transactions;
using HiringManager.EntityFramework;
using HiringManager.Mappers;
using HiringManager.Mappers.Domain;
using HiringManager.Transactions;
using HiringManager.Web.Ninject.Mappers;
using HiringManager.Web.Ninject.Transactions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
                .ToMethod(context =>
                          {
                              if (HttpContext.Current != null)
                                  return HttpContext.Current.User;

                              return System.Threading.Thread.CurrentPrincipal;
                          })
                ;

            //var applicationDbContext = new ApplicationDbContext();
            //var userStore = new UserStore<ApplicationUser>(applicationDbContext);

            Kernel.Bind<IUserStore<ApplicationUser>>()
                .ToMethod(context =>
                                {
                                    var applicationDbContext = new ApplicationDbContext();
                                    var userStore = new UserStore<ApplicationUser>(applicationDbContext);
                                    return userStore;
                                });
        }
    }
}
