using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateOfficeModels.Transactions
{
    public class DealDetailsModel
    {
        public int DealID { get; set; }

        public string PropertyTitle { get; set; } = "";

        public string ClientName { get; set; } = "";

        public DateTime DealDate { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; } = "";
    }

    public class DealDetailModel
    {
        public int DealID { get; set; }

        public string PropertyTitle { get; set; } = "";

        public DateTime DealDate { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; } = "";
    }
}
