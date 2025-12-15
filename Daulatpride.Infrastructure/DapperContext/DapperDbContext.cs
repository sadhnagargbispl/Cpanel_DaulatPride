//using System.Data;
//using System.Data.SqlClient;
//using Daulatpride.Infrastructure.Helper;

//namespace Daulatpride.Infrastructure.DapperContext
//{
//    public class DapperDbContext
//    {
//        private readonly string LiveConn;
//        public DapperDbContext()
//        {
//            LiveConn = ConfigurationManager.AppSetting.GetSection("ConnectionStrings:LiveConn").Value;

//        }
//        public IDbConnection CreateConnection()
//        => new SqlConnection(LiveConn);
//    }
//}
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

        public IDbConnection CreateMainConnection()
            => new SqlConnection(_config.GetConnectionString("MainDB"));

        public IDbConnection CreateSelectConnection()
            => new SqlConnection(_config.GetConnectionString("SelectDB"));
    }
}
