using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateOfficeModels.Auth
{
    public class PasswordResetCode
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string Code { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
