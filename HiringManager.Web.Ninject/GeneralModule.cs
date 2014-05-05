using System.Security.Principal;
using System.Web;
using HiringManager.DomainServices;
using HiringManager.DomainServices.Impl;
using HiringManager.DomainServices.Transactions;
using HiringManager.EntityFramework;
using HiringManager.Transactions;
using HiringManager.Web.Ninject.Transactions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;
using Ninject.Extensions.Conventions;

namespace HiringManager.Web.Ninject
{
    public class GeneralModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IClock>().To<Clock>();
            Bind<IUnitOfWork>().To<UnitOfWork>();

            Kernel.Bind(configuration => configuration
                .FromAssemblyContaining<PositionService>()
                .SelectAllClasses()
                .BindAllInterfaces()
                );

            Kernel.Bind(configuration => configuration
                .FromAssemblyContaining<CreatePosition>()
                .SelectAllClasses()
                .BindAllInterfaces()
                );

            Bind<IFluentTransactionBuilder>()
                .To<FluentTransactionBuilder>()
                ;

            Bind<IDbContext>().To<HiringManagerDbContext>();

            Bind<IPrincipal>()
                .ToMethod(context =>
                          {
                              if (HttpContext.Current != null)
                                  return HttpContext.Current.User;

                              return System.Threading.Thread.CurrentPrincipal;
                          })
                ;

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
