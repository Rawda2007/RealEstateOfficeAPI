using RealStateOfficeModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

using RealEstateOfficeDataAccess;
namespace RealEstateOfficeBusinessLogic
{
    public class AuthBL
    {
        private readonly Autho authDA;


        public AuthBL(Autho authDA)
        {
            this.authDA = authDA;
        }


        public bool EmailExists(string email)
        {
            return authDA.EmailExists(email);
        }

        public int GetUnverifiedUserID(string email)
        {
            Autho da = new Autho();

            return da.GetUnverifiedUserID(email);
        }

        public LoginResult RegisterClient(RegisterModel model)
        {

            // تشفير الباسورد
            string hashPassword =
            BCrypt.Net.BCrypt.HashPassword(
                model.Password
            );


            model.Password = hashPassword;



            // Client فقط
            //int clientRoleID = 3;


            return authDA.RegisterClient(
                model
               
            );

        }

        public LoginResult GetUserByEmail(string email)
        {
            return Autho.GetUserByEmail(email);
        }
        public LoginResult Login(
            string email,
            string password)
        {


            var user =
            Autho.GetUserByEmail(email);



            if (user == null)
            {
                return null;
            }



            bool checkPassword =
            BCrypt.Net.BCrypt.Verify(
                password,
                user.PasswordHash
            );



            if (!checkPassword)
            {
                return null;
            }



            // نشيل الـ Hash قبل ما يطلع بره
            user.PasswordHash = null;


            return user;


        }

        public LoginResult GetUserById(int userID)
        {
            Autho autho = new Autho();
            return autho.GetUserById(userID);
        }

        

        public bool UpdatePassword(
int userID,
string password)
        {

            string hash =
            BCrypt.Net.BCrypt.HashPassword(
            password);


            return authDA.UpdatePassword(
            userID,
            hash);

        }

        public bool VerifyUser(int userID)
        {
            Autho da = new Autho();
            return da.VerifyUser(userID);
        }
    }


}
