using HiringManager.Web.Infrastructure.AutoMapper;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.AutoMapperProfile
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
