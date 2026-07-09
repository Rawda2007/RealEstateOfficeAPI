using RealEstateOfficeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeBusinessLogic
{
    public class EmailVerificationBL
    {

        private EmailVerificationDA da =
        new EmailVerificationDA();



        public void Add(
            int userID,
            string token)
        {
            da.Add(userID, token);
        }

        public bool Verify(
int userID,
string code)
        {
            return da.Verify(
                userID,
                code
            );
        }
    }
}
