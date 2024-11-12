using Microsoft.EntityFrameworkCore;

namespace POSIMSWebApi
{
    public class SerilogContext : DbContext
    {
        public SerilogContext(DbContextOptions<SerilogContext> options) : base(options)
        {
        }
    }
}
