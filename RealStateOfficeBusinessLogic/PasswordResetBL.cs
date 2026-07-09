using RealEstateOfficeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeBusinessLogic
{
    public class PasswordResetBL
    {

        PasswordResetDA da =
        new PasswordResetDA();


        public void Add(int userID, string token)
        {
            da.Add(userID, token);
        }

        public bool Verify(
                string code)
        {
            return da.Verify(code);
        }

        public bool Delete(string code)
        {
            return da.Delete(code);
        }
    }
}
