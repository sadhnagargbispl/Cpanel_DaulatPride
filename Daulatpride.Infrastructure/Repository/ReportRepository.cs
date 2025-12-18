using Daulatpride.Domain.Entities;
using Daulatpride.Domain.Interface;
using Daulatpride.Infrastructure.DapperContext;
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2016.Excel;
using System.Security.Cryptography;
using System.Text;

namespace Daulatpride.Infrastructure.Repository
{
    public class ReportRepository : I_Report
    {
        private readonly DapperDbContext _context;

        public ReportRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<List<CountryType>> GetCountryType()
        {

            using (var connection = _context.CreateSelectConnection())
            {

                var result = await connection.QueryAsync<CountryType>(
                    "Sp_GetCountry",
                    commandType: CommandType.StoredProcedure
                );

                return result.ToList();
            }
        }
        public async Task<List<StateType>> GetStateType()
        {
            using (var connection = _context.CreateSelectConnection())
            {
                var result = await connection.QueryAsync<StateType>(
                    "Sp_GetState",
                    commandType: CommandType.StoredProcedure
                );

                return result.ToList();
            }
        }
        public MemberDto GetMemberByCondition(string condition)
        {
            using (var con = _context.CreateSelectConnection())
            {
                return con.QueryFirstOrDefault<MemberDto>(
                    "sp_MemDtl_update",   // 🔹 NEW PROC NAME
                    new { strWhere = condition },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<bool> IsEmailRegistered(string email)
        {
            try
            {
                using (var connection = _context.CreateSelectConnection())
                {
                    var query = @"SELECT COUNT(*) 
                          FROM daulatpride..M_MemberMaster 
                          WHERE Email = @Email";

                    var result = await connection.ExecuteScalarAsync<int>(
                        query,
                        new { Email = email }
                    );

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("IsEmailRegistered ERROR: " + ex.Message);
            }
        }


        //public async Task<bool> IsMobileRegistered(string mobileNo)
        //{
        //    using (var connection = _context.CreateSelectConnection())  // Create the connection
        //    {
        //        // Query to check if mobile number already exists
        //        var query = "SELECT COUNT(Mobl) FROM daulatpride..M_MemberMaster_Backup WHERE Mobl = @MobileNo";

        //        // Execute the query asynchronously and get the result
        //        var result = await connection.ExecuteScalarAsync<int>(query, new { MobileNo = mobileNo });

        //        // If the result is greater than or equal to 1, return true (mobile number already registered)
        //        return result >= 1;
        //    }
        //}

     
        public string GenerateRandomString(int iLength)
        {
            Random rdm = new Random();
            char[] allowChrs = "123456789".ToCharArray();
            string sResult = "";

            for (int i = 0; i < iLength; i++)
            {
                sResult += allowChrs[rdm.Next(0, allowChrs.Length)];
            }

            return sResult;
        }

        //        public async Task<bool> SaveRegistrationData(Registration model)
        //        {
        //            try
        //            {
        //                using (var con = _context.CreateMainConnection())
        //                {
        //                    string autoPassword = GenerateRandomString(6); // e.g. 6 digit
        //                    string sql = @"
        //INSERT INTO m_memberMaster
        //(
        //        idno,
        //    memfirstname,
        //    memlastname,
        //    formno,
        //    kitid,
        //    RefFormNo,
        //    Address1,
        //    pincode,
        //    mobl,
        //    fax,
        //    panno,
        //    bankname,
        //    acno,
        //    IFSCode,
        //    City,
        //    District,
        //    CountryId,
        //    CountryName,
        //    StateCode,
        //    doj,
        //    UpgrdDSessid,
        //    upgradedate,
        //    NomineeName,
        //    aadharno,
        //    activestatus,
        //    passw,
        //    epassw

        //)
        //VALUES
        //(
        //0,
        //  @MemFirstName,
        //    @MemLastName,
        //    0,
        //    @KitId,
        //    @RefFormNo,
        //    @Address1,
        //    @PinCode,
        //    @MobileNo,
        //    @Fax,
        //    @PanNo,
        //    @BankName,
        //    @AcNo,
        //    @IFSCode,
        //    @City,
        //    @District,
        //    @CountryId,
        //    @Country,
        //    @StateCode,
        //    GETDATE(),
        //    @UpgrdDSessId,
        //    @UpgradeDate,
        //    @NomineeName,
        //    @AadharNo,
        //    'N',
        //    @Password,
        //    @EPassword

        //);";

        //                    var param = new
        //                    {
        //                        IdNo = model.IdNo ?? "",
        //                        MemFirstName = model.MemFirstName ?? "",
        //                        MemLastName = model.MemLastName ?? "",

        //                        FormNo = model.FormNo ?? 0,
        //                        KitId = model.KitId ?? 0,
        //                        RefFormNo = model.RefFormNo ?? 0,

        //                        Address1 = model.Address1 ?? "",
        //                        PinCode = model.PinCode ?? 0,
        //                        MobileNo = model.MobileNo ?? "",
        //                        Fax = model.Fax ?? "",

        //                        PanNo = model.PanNo ?? "",
        //                        BankName = model.BankName ?? "",
        //                        AcNo = model.AcNo ?? "",
        //                        IFSCode = model.IFSCode ?? "",

        //                        City = model.City ?? "",
        //                        District = model.District ?? "",

        //                        CountryId = model.CountryId ?? 0,
        //                        Country = model.Country ?? "",

        //                        StateCode = model.StateCode ?? 0,
        //                        //State = model.State ?? "",

        //                        UpgrdDSessId = model.UpgrdDSessId ?? 0,
        //                        UpgradeDate = DateTime.Now,

        //                        NomineeName = model.NomineeName ?? "",
        //                        AadharNo = model.AadharNo ?? 0,

        //                        Password = autoPassword,
        //                        EPassword = autoPassword



        //                    };

        //                    int rows = await con.ExecuteAsync(sql, param);
        //                    return rows > 0;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Debug.WriteLine("SaveRegistrationData ERROR => " + ex.Message);
        //                throw;
        //            }
        //        }
        public async Task<ProfileResult> SaveRegistrationDataAsync(Registration model)
        {
            int retry = 0;

            while (retry <= 2)
            {
                try
                {
                    string autoPassword = GenerateRandomString(6);

                    // 🔹 1️⃣ INSERT → MainDB
                    using (var con = _context.CreateMainConnection())
                    {
                        string sql = @"
INSERT INTO m_memberMaster
        (
                idno,
            memfirstname,
            memlastname,
            formno,
            kitid,
            RefFormNo,
            Address1,
            pincode,
            mobl,
            fax,
            panno,
            bankname,
            acno,
            IFSCode,
            City,
            District,
            CountryId,
            CountryName,
            StateCode,
            doj,
            UpgrdDSessid,
            upgradedate,
            NomineeName,
            aadharno,
            activestatus,
            passw,
            epassw

        )
        VALUES
        (
        0,
          @MemFirstName,
            @MemLastName,
            0,
            @KitId,
            @RefFormNo,
            @Address1,
            @PinCode,
            @MobileNo,
            @Fax,
            @PanNo,
            @BankName,
            @AcNo,
            @IFSCode,
            @City,
            @District,
            @CountryId,
            @Country,
            @StateCode,
            GETDATE(),
            @UpgrdDSessId,
            @UpgradeDate,
            @NomineeName,
            @AadharNo,
            'N',
            @Password,
            @EPassword
);";

                        int rows = await con.ExecuteAsync(sql, new
                        {
                            IdNo = model.IdNo ?? "",
                            MemFirstName = model.MemFirstName ?? "",
                            MemLastName = model.MemLastName ?? "",

                            FormNo = model.FormNo ?? 0,
                            KitId = model.KitId ?? 0,
                            RefFormNo = model.RefFormNo ?? 0,

                            Address1 = model.Address1 ?? "",
                            PinCode = model.PinCode ?? 0,
                            MobileNo = model.MobileNo ?? "",
                            Fax = model.Fax ?? "",

                            PanNo = model.PanNo ?? "",
                            BankName = model.BankName ?? "",
                            AcNo = model.AcNo ?? "",
                            IFSCode = model.IFSCode ?? "",

                            City = model.City ?? "",
                            District = model.District ?? "",

                            CountryId = model.CountryId ?? 0,
                            Country = model.Country ?? "",

                            StateCode = model.StateCode ?? 0,
                            //State = model.State ?? "",

                            UpgrdDSessId = model.UpgrdDSessId ?? 0,
                            UpgradeDate = DateTime.Now,

                            NomineeName = model.NomineeName ?? "",
                            AadharNo = model.AadharNo ?? 0,

                            Password = autoPassword,
                            EPassword = autoPassword
                        });

                        if (rows <= 0)
                            throw new Exception("Insert failed");
                    }

                    // 🔹 2️⃣ FETCH → SelectDB (Stored Procedure)
                    using (var conSelect = _context.CreateSelectConnection())
                    {
                        var profile = await conSelect.QueryFirstOrDefaultAsync<ProfileResult>(
                            "EXEC Sp_GetProfile",
                            new { RefFormNo = model.RefFormNo }
                        );

                        if (profile == null)
                            throw new Exception("Profile not found in SelectDB");

                        return profile; // ✅ SUCCESS
                    }
                }
                catch
                {
                    retry++;
                    if (retry > 2)
                        throw;
                }
            }

            throw new Exception("Registration failed after retries");
        }


        public async Task<SponsorInfo> GetSponsorNameAsync(string sponsorId)
        {
            using (var connection = _context.CreateSelectConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<dynamic>(
                    "Sp_GetSponsorName",
                    new { SponsorId = sponsorId },
                    commandType: CommandType.StoredProcedure
                );

                if (result == null)
                    return null;

                return new SponsorInfo
                {
                    SponsorId = sponsorId,
                    FullName = result.FullName?.ToString(),
                    FormNo = result.FormNo == null ? 0 : (int)result.FormNo,
                    KitId = result.KitId == null ? 0 : (int)result.KitId
                };
            }
        }
        public async Task<Dashboard> LoadDashboard(string formNo)
        {
            using var connection = _context.CreateSelectConnection();
            var model = new Dashboard();

            using var multi = await connection.QueryMultipleAsync(
                "sp_LoadTeamNewUpdate",
                new { FormNo = formNo },
                commandType: CommandType.StoredProcedure);

            // 1️⃣ sp_IncomeOld
            await multi.ReadAsync<dynamic>();

            // 2️⃣ sp_MyDirect
            await multi.ReadAsync<dynamic>();

            // 3️⃣ User Info
            var user = await multi.ReadFirstOrDefaultAsync<UserInfoVM>();
            if (user != null)
            {
                model.UserName = user.Name;
                model.UserId = user.IdNo;
                model.DOJ = user.DOj;
                model.SponsorId = user.SponsorId;
                model.SponsorName = user.SponsorName;
                // User Info read hone ke baad
             //   model.ReferralLink =
             //baseUrl + "/Registration/Registration?ref=" +
             //CryptoHelper.Encrypt(user.Mid + "/0/D");

             //   model.ReferralLinkClient =
             //       baseUrl + "/Registration/Registration?ref=" +
             //       CryptoHelper.Encrypt(user.Mid + "/0/A");

            }

            // 4️⃣ Sp_MyTeambsiness
            await multi.ReadAsync<dynamic>();

            // 5️⃣ Sp_GetinvestmentUpdate
            await multi.ReadAsync<dynamic>();

            // 6️⃣ Wallets
            model.Wallets = (await multi.ReadAsync<Wallet>()).ToList();

            // 7️⃣ Sp_MyTeamDetailNew
            await multi.ReadAsync<dynamic>();

            // 8️⃣ News
            model.News = (await multi.ReadAsync<News>()).ToList();

            // 9️⃣ Rank
            await multi.ReadAsync<dynamic>();

            // 🔟 Popup
            await multi.ReadAsync<dynamic>();

            // 1️⃣1️⃣ OpenLevel
            await multi.ReadAsync<dynamic>();

            // 1️⃣2️⃣ Widgets (Name, Direct, Icon, Div)
            model.Widgets = (await multi.ReadAsync<Widget>()).ToList();

            // 1️⃣3️⃣ Direct Members
            model.DirectMembers = (await multi.ReadAsync<DirectMemberV>()).ToList();

            // 1️⃣4️⃣ Total Withdrawals
            model.TotalWithdrawals = (await multi.ReadAsync<TotalWithdrawal>()).ToList();
            model.WalletSummarys = (await multi.ReadAsync<WalletSummary>()).ToList();
            model.LeftRightSummarys = (await multi.ReadAsync<LeftRightSummary>()).ToList();

            return model;
        }

        public static class CryptoHelper
        {
            private static readonly byte[] Key =
                Encoding.ASCII.GetBytes("6b04d38748f94490a636cf1be3d82841");

            private static readonly byte[] IV =
                Encoding.ASCII.GetBytes("f8adbf3c94b7463d");

            public static string Encrypt(string plainText)
            {
                byte[] encrypted;
                using (AesManaged aes = new AesManaged())
                {
                    var encryptor = aes.CreateEncryptor(Key, IV);
                    using (var ms = new MemoryStream())
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                        sw.Close();
                        encrypted = ms.ToArray();
                    }
                }
                return Convert.ToBase64String(encrypted);
            }

            public static string Decrypt(string data)
            {
                byte[] cipherText = Convert.FromBase64String(data);
                using (AesManaged aes = new AesManaged())
                {
                    var decryptor = aes.CreateDecryptor(Key, IV);
                    using (var ms = new MemoryStream(cipherText))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }


    }
}
