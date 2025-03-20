using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHR.Persistence.Context;

namespace SGHR.Application.Test.Base
{
    public abstract class BaseTestService<T, R> where R : class
    {
        protected readonly Mock<R> _mockRepository;
        protected readonly Mock<ILogger<T>> _mockLogger;
        protected readonly Mock<IConfiguration> _mockConfiguration;

        protected BaseTestService()
        {
            _mockRepository = new Mock<R>();
            _mockLogger = new Mock<ILogger<T>>();
            _mockConfiguration = new Mock<IConfiguration>();

            // Setup configuration mock with common keys
            var configurationSectionMock = new Mock<IConfigurationSection>();
            configurationSectionMock.Setup(x => x.Value).Returns("Mock error message");

            _mockConfiguration.Setup(x => x[It.IsAny<string>()]).Returns("Mock error message");
        }
    }
}