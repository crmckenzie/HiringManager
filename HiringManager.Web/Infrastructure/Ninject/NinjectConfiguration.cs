using System.Web;
using HiringManager.DomainServices;
using HiringManager.DomainServices.Authentication;
using HiringManager.DomainServices.Validators;
using HiringManager.Web.Infrastructure.App;
using HiringManager.Web.Ninject;
using Microsoft.Owin.Security;
using Ninject;
using Simple.Validation.Ninject;
using Ninject.Extensions.Conventions;

namespace HiringManager.Web.Infrastructure.Ninject
{
    public class NinjectConfiguration
    {
        public void Configure(IKernel kernel)
        {
            kernel.Load(new GeneralModule());
            kernel.Load(new SimpleValidationNinjectModule());

            kernel.Bind<IUploadService>().To<UploadService>();
            kernel.Bind<IUserManager>().To<ClaimsIdentityUserManager>();

            kernel.Bind(configuration => configuration
                .FromAssemblyContaining<NewCandidateRequestValidator>()
                .SelectAllClasses()
                .BindAllInterfaces()
                );

            kernel.Bind<IAuthenticationManager>()
                .ToMethod(context => HttpContext.Current.GetOwinContext().Authentication);


        }
    }
}