using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateOfficeModels.Transactions
{
    public class Deal
    {
        public int DealID { get; set; }


        public int PropertyID { get; set; }

        public int ClientID { get; set; }


        public DateTime DealDate { get; set; }


        public decimal Amount { get; set; }
    }
}
