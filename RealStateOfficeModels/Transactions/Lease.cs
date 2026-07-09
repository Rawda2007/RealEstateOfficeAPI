using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateOfficeModels.Transactions
{
    public class Lease
    {
        public int LeaseID { get; set; }


        public int DealID { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }


        public decimal MonthlyRent { get; set; }
    }
}
