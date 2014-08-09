using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringManager.DomainServices.Authentication;
using HiringManager.Web.Infrastructure.Ninject;
using Ninject;
using NUnit.Framework;

namespace HiringManager.Web.Integration.Tests.Infrastructure.Ninject
{
    [TestFixture]
    public class NinjectBindingsTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
            this.Kernel = new StandardKernel();
            new NinjectConfiguration().Configure(this.Kernel);
        }

        public StandardKernel Kernel { get; set; }

        [Test]
        public void IUserManager()
        {
            // Arrange

            // Act
            var result = this.Kernel.Get<IUserManager>();

            // Assert
        }
    }
}
