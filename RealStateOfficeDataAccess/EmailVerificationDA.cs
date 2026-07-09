using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeDataAccess
{
    public class EmailVerificationDA
    {
        public void Add(int userID, string token)
        {
            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            con.Open();


            SqlTransaction transaction =
            con.BeginTransaction();


            try
            {

                SqlCommand deleteCmd =
                new SqlCommand(
                @"
        DELETE FROM EmailVerificationTokens
        WHERE UserID=@UserID
        ",
                con,
                transaction);


                deleteCmd.Parameters.AddWithValue(
                "@UserID",
                userID);


                deleteCmd.ExecuteNonQuery();



                SqlCommand insertCmd =
                new SqlCommand(
                @"
        INSERT INTO EmailVerificationTokens
        (
            UserID,
            Token,
            ExpiryDate
        )

        VALUES
        (
            @UserID,
            @Token,
            @ExpiryDate
        )
        ",
                con,
                transaction);



                insertCmd.Parameters.AddWithValue(
                "@UserID",
                userID);


                insertCmd.Parameters.AddWithValue(
                "@Token",
                token);


                insertCmd.Parameters.AddWithValue(
                "@ExpiryDate",
                DateTime.Now.AddMinutes(15));


                insertCmd.ExecuteNonQuery();



                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public bool Verify(
   int userID,
   string code)
        {

            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            string query =
            @"
    SELECT COUNT(*)
    FROM EmailVerificationTokens
    WHERE UserID=@UserID
    AND Token=@Token
    AND ExpiryDate > GETDATE()
    ";


            SqlCommand cmd =
            new SqlCommand(
            query,
            con);



            cmd.Parameters.AddWithValue(
            "@UserID",
            userID);



            cmd.Parameters.AddWithValue(
            "@Token",
            code);



            con.Open();


            int count =
            (int)cmd.ExecuteScalar();


            return count > 0;
        }
    }
}
