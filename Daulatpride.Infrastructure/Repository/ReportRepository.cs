using Daulatpride.Domain.Entities;
using Daulatpride.Domain.Interface;
using Daulatpride.Infrastructure.DapperContext;
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

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

        //        public async Task<bool> SaveRegistrationData(Registration model)
        //        {
        //            try
        //            {
        //                using (var con = _context.CreateMainConnection())
        //                {
        //                    string sql = @"
        // insert into m_memberMaster(
        //   idno,memfirstname,memlastname,formno,kitid,RefFormNo,Address1,pincode,mobl,fax,panno,bankname,
        // acno,IFSCode,District,doj,UpgrdDSessid,upgradedate,NomineeName,aadharno,activestatus,passw,epassw)
        //VALUES
        //(
        //    @SessId, 0, 0, 0, @KitId, @UpLnFormNo, 0, @LegNo, 0, @RefFormNo,
        //    @MemFirstName, '', @MemRelation, @MemFName, @MemDOB, @MemGender, '',
        //    @NomineeName, @Address1, '', '', @Tehsil, @City, @District, @StateCode, @CountryId,
        //    @PinCode, @PhN1, 0, @MobileNo, @MarrgDate, @Password, GETDATE(),
        //    @Relation, @PanNo, @BankID, @MICRCode, @BranchName, @EmailId,
        //    @BV, 0, @Password, @Password, 'Y', @BillNo, @RP, @HostIp,
        //    0, @PayMode, @ChequeNo, 0, @ChequeBank, @ChequeDate, @ChequeBranch,
        //    'N', @Aadhar1, @Aadhar2, @Aadhar3, @TransactionId,
        //    @WalletAddress, @UserCode, @RegType,@TransNo
        //)";

        //                    var param = new
        //                    {
        //                        SessId = 1,

        //                        KitId = model.KitId ?? 0,
        //                        TransNo = model.TransNo ?? 0,
        //                        UpLnFormNo = model.UpLnFormNo ?? 0,
        //                        LegNo = model.LegNo ?? 0,
        //                        RefFormNo = model.RefFormNo ?? 0,

        //                        MemFirstName = model.FullName ?? "",
        //                        MemRelation = model.MemRelation ?? "",
        //                        MemFName = model.FatherName ?? "",
        //                        MemDOB = model.DOB ?? new DateTime(1900, 1, 1),
        //                        MemGender = model.Gender ?? "",

        //                        NomineeName = model.NomineeName ?? "",
        //                        Address1 = model.Address ?? "",

        //                        Tehsil = model.City ?? "",
        //                        City = model.City ?? "",
        //                        District = model.District ?? "",

        //                        StateCode = model.StateCode ?? 0,
        //                        CountryId = model.CountryId ?? 0,
        //                        PinCode = string.IsNullOrEmpty(model.PinCode) ? 0 : Convert.ToInt32(model.PinCode),
        //                        PhN1 = string.IsNullOrEmpty(model.PhoneNo) ? 0 : Convert.ToInt64(model.PhoneNo),
        //                        MobileNo = string.IsNullOrEmpty(model.MobileNo) ? 0 : Convert.ToInt64(model.MobileNo),

        //                        MarrgDate = model.MarriageDate ?? new DateTime(1900, 1, 1),
        //                        Relation = string.IsNullOrWhiteSpace(model.Relation) ? "SELF" : model.Relation,
        //                        PanNo = model.PanNo ?? "",

        //                        Password = model.Password ?? "",
        //                        EmailId = model.EmailId ?? "",

        //                        BankID = model.BankId ?? 0,
        //                        MICRCode = model.MICR ?? "",
        //                        BranchName = model.BranchName ?? "",

        //                        BV = model.BV ?? 0,
        //                        BillNo = model.InvoiceNo ?? 0,
        //                        RP = model.RP ?? 0,

        //                        HostIp = model.HostIp ?? "",
        //                        PayMode = model.PayModeId ?? 0,

        //                        ChequeNo = model.ChequeNo ?? 0,
        //                        ChequeBank = model.ChequeBank ?? "",
        //                        ChequeDate = model.ChequeDate ?? new DateTime(1900, 1, 1),
        //                        ChequeBranch = model.ChequeBranch ?? "",

        //                        Aadhar1 = model.Aadhar1 ?? 0,
        //                        Aadhar2 = model.Aadhar2 ?? 0,
        //                        Aadhar3 = model.Aadhar3 ?? 0,

        //                        TransactionId = model.TransactionId ?? "",
        //                        WalletAddress = model.WalletAddress?.ToString() ?? "",

        //                        UserCode = model.UserCode ?? 0,
        //                        RegType = model.RegType ?? 0
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


    }
}
