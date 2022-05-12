using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberAndOrganizationDataCorrectionInEBS.Model
{
    public class MemberOrOrganizationAccountDetailsDto
    {
        public string AccountNumber { get; set; }

        public string PartyId { get; set; }

        public string AccountType { get; set; }

        public string AccountName { get; set; }

        public string DunsNumber { get; set; }
    }
}
