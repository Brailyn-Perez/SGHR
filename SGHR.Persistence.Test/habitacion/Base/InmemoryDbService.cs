using Microsoft.EntityFrameworkCore;
using SGHR.Persistence.Context;

namespace SGHR.Persistence.Test.habitacion.Base
{
    public static class InMemoryDbService
    {
        public static SGHRContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<SGHRContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            return new SGHRContext(options);
        }
    }

}