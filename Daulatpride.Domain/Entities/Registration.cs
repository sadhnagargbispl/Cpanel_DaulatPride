using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daulatpride.Domain.Entities
{
    public class Registration
    {
           public string EmailId { get; set; }
        public string MobileNo { get; set; }

        public string Country { get; set; }
        public int? CountryId { get; set; }

        public string State { get; set; }
        public int? StateCode { get; set; }

        public string City { get; set; }
        public string District { get; set; }


        // ================= PERSONAL =================
        public DateTime? DOB { get; set; }
        public DateTime? MarriageDate { get; set; }

        public string SponsorId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }

        public string MemRelation { get; set; }
        public string FatherName { get; set; }
        public string NomineeName { get; set; }

        public string Address { get; set; }
        public string PhoneNo { get; set; }

        public string Relation { get; set; }
        public string PanNo { get; set; }

        public bool IsTermsAccepted { get; set; }

        // ================= REGISTRATION =================
        public int? KitId { get; set; }
        public int? UpLnFormNo { get; set; }
        public int? LegNo { get; set; }
        public int? RefFormNo { get; set; }

        public string Password { get; set; }

        // ================= BANK / PAYMENT =================
        public int? BankId { get; set; }
        public string MICR { get; set; }
        public string BranchName { get; set; }

        public int? BV { get; set; }
        public int? InvoiceNo { get; set; }   // billNo
        public int? RP { get; set; }

        public int? PayModeId { get; set; }

        public long? ChequeNo { get; set; }
        public string ChequeBank { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string ChequeBranch { get; set; }

        // ================= IDENTITY =================
        public long? Aadhar1 { get; set; }
        public long? Aadhar2 { get; set; }
        public long? Aadhar3 { get; set; }

        // ================= SYSTEM =================
        public string TransactionId { get; set; }
        public long? WalletAddress { get; set; }
        public int? UserCode { get; set; }
        public int? RegType { get; set; }

        public string HostIp { get; set; }
        public string IdNo { get; set; }
        public int? PinCode { get; set; }
        public int? FormNo { get; set; }
    
        // ===== MEMBER NAME =====
        public string MemFirstName { get; set; }
        public string MemLastName { get; set; }

        // ===== ADDRESS / CONTACT =====
        public string Address1 { get; set; }

        public long? Mobl { get; set; }
        public string Fax { get; set; }
     


        public string BankName { get; set; }
        public string AcNo { get; set; }
        public string IFSCode { get; set; }

        // ===== UPGRADE =====
        public int? UpgrdDSessId { get; set; }
        public DateTime? UpgradeDate { get; set; }

      
        public long? AadharNo { get; set; }

        // ===== LOGIN =====
        public string Passw { get; set; }
    }


}
