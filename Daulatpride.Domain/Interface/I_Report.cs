using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daulatpride.Domain.Entities;
using DocumentFormat.OpenXml.Vml;

namespace Daulatpride.Domain.Interface
{

    public interface I_Report
    {
        Task<List<CountryType>> GetCountryType();
        Task<List<StateType>> GetStateType();
        Task<SponsorInfo> GetSponsorNameAsync(string sponsorId);
        Task<bool> IsEmailRegistered(string email);
        //Task<bool> IsMobileRegistered(string mobileNo);
        Task<ProfileResult> SaveRegistrationDataAsync(Registration model);
        MemberDto GetMemberByCondition(string condition);



    }
}
