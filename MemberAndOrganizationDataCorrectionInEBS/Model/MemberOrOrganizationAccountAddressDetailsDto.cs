using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberAndOrganizationDataCorrectionInEBS.Model
{
    public class MemberOrOrganizationAccountAddressDetailsDto
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string Address4 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string SiteUseCode { get; set; }

        public string SiteId { get; set; }

        public string SiteUseId { get; set; }

        public string Status { get; set; }

        public string PrimaryFlag { get; set; }
    }
}
