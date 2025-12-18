using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daulatpride.Domain.Entities
{
    public class Dashboard
    {
        public List<DirectMemberV> DirectMembers { get; set; }
        public List<Widget> Widgets { get; set; }
        public List<Wallet> Wallets { get; set; }
        public List<News> News { get; set; }
        public List<TotalWithdrawal> TotalWithdrawals { get; set; }
        public List<WalletSummary> WalletSummarys { get; set; }
        public List<LeftRightSummary> LeftRightSummarys { get; set; }

        public string UserName { get; set; }
        public string UserId { get; set; }
        public string DOJ { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }

        public string MainLeg { get; set; }
        public string OtherLeg { get; set; }

        public string ReferralLink { get; set; }       
        public string ReferralLinkClient { get; set; }
        public string baseUrl { get; set; }
    }
}
