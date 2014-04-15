using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringManager.Web.Infrastructure.AutoMapper;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.AutoMapper
{
    [TestFixture]
    public class AutoMapperConfigurationTests
    {
        [SetUp]
        public void BeforeEachTestRuns()
        {
            AutoMapperConfiguration.Configure();
        }

        [Test]
        public void ConfigurationIsValid()
        {
            // Arrange

            // Act
            global::AutoMapper.Mapper.AssertConfigurationIsValid();

            // Assert
        }
    }
}
