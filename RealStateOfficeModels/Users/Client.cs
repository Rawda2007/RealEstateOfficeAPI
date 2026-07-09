using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateOfficeModels.Users
{
    public class Client
    {
        public int ClientID { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }

        //public int PurposeID { get; set; }
    }
}
