using Microsoft.Data.SqlClient;
using RealStateOfficeModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealEstateOfficeDataAccess
{
    public class RefreshTokenDA
    {

        public bool Add(
            int userID,
            string token)
        {

            using (SqlConnection con =
                new SqlConnection(
                DbConnection.StringConnection))
            {

                string query =
                @"
            INSERT INTO RefreshTokens
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
            ";


                SqlCommand cmd =
                new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@UserID",
                    userID
                );


                cmd.Parameters.AddWithValue(
                    "@Token",
                    token
                );


                cmd.Parameters.AddWithValue(
                    "@ExpiryDate",
                    DateTime.Now.AddDays(7)
                );


                con.Open();


                return cmd.ExecuteNonQuery() > 0;
            }

        }


        public RefreshToken Get(string token)
        {

            RefreshToken refresh = null;


            using (SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection))
            {

                string query =
                @"
        SELECT 
        UserID,
        Token,
        ExpiryDate,
        IsRevoked

        FROM RefreshTokens

        WHERE Token=@Token
        ";


                SqlCommand cmd =
                new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@Token",
                    token
                );


                con.Open();


                SqlDataReader reader =
                cmd.ExecuteReader();


                if (reader.Read())
                {
                    refresh = new RefreshToken
                    {
                        UserID =
                        Convert.ToInt32(reader["UserID"]),


                        Token =
                        reader["Token"].ToString(),


                        ExpiryDate =
                        Convert.ToDateTime(
                        reader["ExpiryDate"]),


                        IsRevoked =
                        Convert.ToBoolean(
                        reader["IsRevoked"])
                    };
                }

            }


            return refresh;
        }


        public bool Revoke(string token)
        {

            using (SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection))
            {

                string query =
                @"
        UPDATE RefreshTokens

        SET IsRevoked = 1

        WHERE Token=@Token
        ";


                SqlCommand cmd =
                new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@Token",
                    token
                );


                con.Open();


                return cmd.ExecuteNonQuery() > 0;

            }

        }
    }
}
