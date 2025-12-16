
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
        public async Task<MemberInfo> ValidateUser(Login model)
        {
            using (var connection = _context.CreateSelectConnection())
            {
                var param = new DynamicParameters();
                param.Add("@UserID", model.Username);
                param.Add("@Password", model.Password);

                // SP se data
                var user = await connection.QueryFirstOrDefaultAsync<User>(
                    "sp_Login_New",
                    param,
                    commandType: CommandType.StoredProcedure
                );

                // ❌ Agar record nahi mila
                if (user == null)
                    return null;

                // ✅ SP result → MemberInfo set
                var member = new MemberInfo
                {
                    IDNo = user.IDNo,
                    FormNo = user.Formno,
                    MemName = $"{user.MemFirstName ?? ""} {user.MemLastName ?? ""}".Trim(),

                    MobileNo = user.Mobl,
                    MemKit = user.KitID,
                    Package = user.KitName,
                    Position = user.fld3,

                    Doj = user.Doj,
                    DOA = user.UpgradeDate == null
                            ? ""
                            : user.UpgradeDate.Value.ToString("dd-MMM-yyyy"),

                    Address = user.Address1,
                    IsFranchise = user.Fld5,
                    ActiveStatus = user.ActiveStatus,

                    MID = user.MID,
                    EMail = user.Email,
                    ProfilePic = user.profilepic,
                    Panno = user.Panno,

                    ActivationDate = user.UpgradeDate,
                    Type = "A",
                    MemPassw = user.MemPassw,
                    MemEPassw = user.MemEPassw
                };

                return member; // ✅ object return
            }
        }

        //public async Task<MemberInfo> ValidateUser(Login model)
        //{
        //    using (var connection = _context.CreateSelectConnection()) // ✅ FIX
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("@UserID", model.Username);
        //        param.Add("@Password", model.Password);

        //        var user = await connection.QueryFirstOrDefaultAsync<User>(
        //            "sp_Login_New",
        //            param,
        //            commandType: CommandType.StoredProcedure
        //        );

        //        return MemberInfo;
        //    }
        //}
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