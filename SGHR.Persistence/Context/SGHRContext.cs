
using Microsoft.EntityFrameworkCore;

namespace SGHR.Persistence.Context
{
    public class SGHRContext : DbContext
    {
        public SGHRContext(DbContextOptions options) : base(options)
        {
        }
    }
}
