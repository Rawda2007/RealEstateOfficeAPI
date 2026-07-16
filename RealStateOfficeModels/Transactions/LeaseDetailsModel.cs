using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateOfficeModels.Transactions
{
    public class LeaseDetailsModel
    {
        public int LeaseID { get; set; }

        public string PropertyTitle { get; set; } = string.Empty;

        public string ClientName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal MonthlyRent { get; set; }
    }
}
