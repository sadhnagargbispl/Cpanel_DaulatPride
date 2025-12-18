using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Daulatpride.Infrastructure.DapperContext
{
    public class DapperDbContext
    {
        private readonly IConfiguration _config;

        public DapperDbContext(IConfiguration config)
        {
            _config = config;
        }

        // 🔹 MAIN DATABASE (daulatpride)
        public IDbConnection CreateMainConnection()
            => new SqlConnection(_config.GetConnectionString("MainDB"));

        // 🔹 SELECT / BACKUP DATABASE (daulatprideselect)
        public IDbConnection CreateSelectConnection()
            => new SqlConnection(_config.GetConnectionString("SelectDB"));
    }
}
