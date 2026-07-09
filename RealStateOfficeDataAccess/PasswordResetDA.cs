using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeDataAccess
{
    public class PasswordResetDA
    {
        public void Add(
int userID,
string code)
        {

            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            con.Open();


            SqlTransaction transaction =
            con.BeginTransaction();


            try
            {

                // Delete old code
                SqlCommand deleteCmd =
                new SqlCommand(
                @"
    DELETE FROM PasswordResetCodes
    WHERE UserID=@UserID
    ",
                con,
                transaction);



                deleteCmd.Parameters.AddWithValue(
                "@UserID",
                userID);



                deleteCmd.ExecuteNonQuery();



                // Insert new code
                SqlCommand insertCmd =
                new SqlCommand(
                @"
    INSERT INTO PasswordResetCodes
    (
      UserID,
      Code,
      ExpiryDate
    )

    VALUES
    (
      @UserID,
      @Code,
      @ExpiryDate
    )
    ",
                con,
                transaction);



                insertCmd.Parameters.AddWithValue(
                "@UserID",
                userID);


                insertCmd.Parameters.AddWithValue(
                "@Code",
                code);


                insertCmd.Parameters.AddWithValue(
                "@ExpiryDate",
                DateTime.Now.AddMinutes(10));



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
string code)
        {

            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            string query =
            @"
SELECT COUNT(*)
FROM PasswordResetCodes
WHERE Code=@Code
AND ExpiryDate > GETDATE()
";


            SqlCommand cmd =
            new SqlCommand(query, con);


            cmd.Parameters.AddWithValue(
            "@Code",
            code);



            con.Open();


            int count =
            (int)cmd.ExecuteScalar();


            return count > 0;

        }

        public bool Delete(string code)
        {
            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            string query =
            @"
    DELETE FROM PasswordResetCodes
    WHERE Code=@Code
    ";


            SqlCommand cmd =
            new SqlCommand(query, con);


            cmd.Parameters.AddWithValue(
                "@Code",
                code
            );


            con.Open();


            int rows =
            cmd.ExecuteNonQuery();


            return rows > 0;
        }
    }
}
