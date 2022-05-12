using System;

namespace MemberAndOrganizationDataCorrectionInEBS.Model
{
    public class MemberOrOrganizationAccountRelationshipDetailsDto
    {
        public string AccountNumber { get; set; }

        public string RelationshipType { get; set; }

        public string ContactId { get; set; }

        public string RelationshipStatus { get; set; }

        public string StartDate { get; set; }

        public DateTime Start_Date { get; set; }

        public string EndDate { get; set; }

        public DateTime End_Date { get; set; }
    }
}
