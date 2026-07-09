using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateOfficeModels.Auth
{
    public class EmailVerificationToken
    {
        public int UserID { get; set; }

        public string Token { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}
