using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberAndOrganizationDataCorrectionInEBS.Model
{
    public class MemberAndOrganizationAccountDetail
    {
        public string MemberAccountNumber { get; set; }

        public string OrgAccountNumber { get; set; }

        public long MemberContactId { get; set; }
    }
}
