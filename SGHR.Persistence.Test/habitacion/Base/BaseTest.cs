using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHR.Persistence.Context;

namespace SGHR.Persistence.Test.habitacion.Base
{
    public abstract class BaseTest<T> 
    {
        protected readonly SGHRContext _context;
        protected readonly Mock<ILogger<T>> _mockLogger;
        protected readonly Mock<IConfiguration> _mockConfiguration;

        protected BaseTest()
        {
            _context = InMemoryDbService.CreateContext();
            _mockLogger = new Mock<ILogger<T>>();
            _mockConfiguration = new Mock<IConfiguration>();
        }
    }
}
