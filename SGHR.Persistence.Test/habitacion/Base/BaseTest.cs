using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHR.Persistence.Context;
using SGHR.Persistence.Repositories.habitacion;

namespace SGHR.Persistence.Test.habitacion.Base
{
    public abstract class BaseTest
    {
        protected readonly SGHRContext _context;
        protected readonly Mock<ILogger<CategoriaRepository>> _mockLogger;
        protected readonly Mock<IConfiguration> _mockConfiguration;

        protected BaseTest()
        {
            _context = InMemoryDbService.CreateContext();
            _mockLogger = new Mock<ILogger<CategoriaRepository>>();
            _mockConfiguration = new Mock<IConfiguration>();
        }
    }
}
