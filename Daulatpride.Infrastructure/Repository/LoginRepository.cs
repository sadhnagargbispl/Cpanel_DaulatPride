
using Daulatpride.Domain.Entities;
using Daulatpride.Domain.Interface;
using Daulatpride.Infrastructure.DapperContext;
using Dapper;
using System.Data;

namespace Daulatpride.Infrastructure.Repository
{
    public class LoginRepository : I_Login
    {
        private readonly DapperDbContext _context;

        public LoginRepository(DapperDbContext context)
        {
            _context = context;
        }

        // Agar interface me required hai to rehne do
        public Task<Login> GetLogin(Login req)
        {
            throw new NotImplementedException();
        }

        public async Task<User> ValidateUser(Login model)
        {
            using (var connection = _context.CreateSelectConnection()) // ✅ FIX
            {
                var param = new DynamicParameters();
                param.Add("@UserID", model.Username);
                param.Add("@Password", model.Password);

                var user = await connection.QueryFirstOrDefaultAsync<User>(
                    "sp_Login_New",
                    param,
                    commandType: CommandType.StoredProcedure
                );

                return user;
            }
        }
    }
}
//using Daulatpride.Domain.Entities;
//using Daulatpride.Domain.Interface;
//using Daulatpride.Infrastructure.DapperContext;
//using Dapper;
//using System.Data;
//namespace Daulatpride.Infrastructure.Repository
//{
//    public class LoginRepository : I_Login
//    {
//        private readonly DapperDbContext _context;

//        public LoginRepository(DapperDbContext context)
//        {
//            _context = context;
//        }

//        public Task<Login> GetLogin(Login req)
//        {
//            throw new NotImplementedException();
//        }
//        public async Task<User> ValidateUser(Login model)
//        {
//            using (var connection = _context.CreateConnection())
//            {
//                var param = new DynamicParameters();
//                param.Add("@UserID", model.Username);
//                param.Add("@Password", model.Password);

//                var user = await connection.QueryFirstOrDefaultAsync<User>(
//                    "sp_Login_New",
//                    param,
//                    commandType: CommandType.StoredProcedure
//                );

//                return user;
//            }
//        }


//    }

//}
//}