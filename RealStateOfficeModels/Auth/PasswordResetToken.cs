using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateOfficeModels.Auth
{
    public class PasswordResetToken
    {
        public int ResetID { get; set; }

        public int UserID { get; set; }

        public string Token { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
