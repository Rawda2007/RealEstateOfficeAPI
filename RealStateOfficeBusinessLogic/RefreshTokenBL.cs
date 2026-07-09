using RealEstateOfficeDataAccess;
using RealStateOfficeModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealEstateOfficeBusinessLogic
{
    public class RefreshTokenBL
    {
        private readonly RefreshTokenDA refreshTokenDA;


        public RefreshTokenBL(
            RefreshTokenDA refreshTokenDA)
        {
            this.refreshTokenDA = refreshTokenDA;
        }
        public RefreshTokenBL()
            {
            }


        public  bool Add(
            int userID,
            string token)
        {
            return refreshTokenDA.Add(
                userID,
                token
            );
        }

        public RefreshToken Get(
      string token)
        {
            return refreshTokenDA.Get(
                token
            );
        }


        public bool Revoke(string token)
        {
            return refreshTokenDA.Revoke(token);
        }

    }
}

