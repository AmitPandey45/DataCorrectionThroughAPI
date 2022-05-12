using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MemberAndOrganizationDataCorrectionInEBS.Extension
{
    public static class DataColumnCollectionExtensions
    {
        public static IEnumerable<DataColumn> AsEnumerable(this DataColumnCollection source)
        {
            return source.Cast<DataColumn>();
        }
    }
}
