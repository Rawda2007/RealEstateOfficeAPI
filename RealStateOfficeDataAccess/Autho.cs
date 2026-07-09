using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealStateOfficeModels.Auth;
 
namespace RealEstateOfficeDataAccess
{
    public class Autho
    {
        public bool EmailExists(string email)
        {
            using (SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection))
            {

                string query =
                @"
        SELECT COUNT(*)
        FROM Users
        WHERE Email=@Email AND IsVerified = 1
        ";


                SqlCommand cmd =
                new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@Email",
                    email
                );


                con.Open();


                int count =
                (int)cmd.ExecuteScalar();


                return count > 0;
            }
        }

        public int GetUnverifiedUserID(string email)
        {
            using SqlConnection con =
            new SqlConnection(DbConnection.StringConnection);


            string query =
            @"
    SELECT UserID
    FROM Users
    WHERE Email=@Email
    AND IsVerified=0
    ";


            SqlCommand cmd =
            new SqlCommand(query, con);


            cmd.Parameters.AddWithValue(
                "@Email",
                email
            );


            con.Open();


            var result = cmd.ExecuteScalar();


            if (result == null)
                return 0;


            return Convert.ToInt32(result);
        }
        public LoginResult RegisterClient(RegisterModel model, int roleID=3)
        {
            LoginResult user = null;


            using (SqlConnection con =
                new SqlConnection(DbConnection.StringConnection))
            {

                con.Open();


                SqlTransaction transaction =
                    con.BeginTransaction();


                try
                {

                    // 1) Insert User
                    string userQuery =
                    @"
            INSERT INTO Users
            (
                Username,
                Email,
                Phone,
                PasswordHash,
                RoleID
            )

            VALUES
            (
                @Username,
                @Email,
                @Phone,
                @PasswordHash,
                @RoleID
            );

            SELECT SCOPE_IDENTITY();
            ";



                    SqlCommand cmd =
                    new SqlCommand(
                        userQuery,
                        con,
                        transaction
                    );


                    cmd.Parameters.AddWithValue(
                        "@Username",
                        model.Username
                    );

                    cmd.Parameters.AddWithValue(
                        "@Email",
                        model.Email
                    );

                    cmd.Parameters.AddWithValue(
                        "@Phone",
                        model.Phone
                    );

                    cmd.Parameters.AddWithValue(
                        "@PasswordHash",
                        model.Password
                    );

                    cmd.Parameters.AddWithValue(
                        "@RoleID",
                        roleID
                    );


                    int userID =
                    Convert.ToInt32(
                        cmd.ExecuteScalar()
                    );



                    // 2) Insert Client
                    string clientQuery =
                    @"
            INSERT INTO Client
            (
                UserID,PurposeID
            )

            VALUES
            (
                @UserID,1
            )
            ";


                    SqlCommand clientCmd =
                    new SqlCommand(
                        clientQuery,
                        con,
                        transaction
                    );


                    clientCmd.Parameters.AddWithValue(
                        "@UserID",
                        userID
                    );


                    clientCmd.ExecuteNonQuery();



                    // 3) Get User Data
                    string selectQuery =
                    @"
            SELECT 
    Users.UserID,
    Users.Username,
    Users.Email,
    Users.Phone,
    Role.RoleName

FROM Users

INNER JOIN Role
ON Users.RoleID = Role.RoleID

WHERE Users.UserID=@UserID
            ";


                    SqlCommand selectCmd =
                    new SqlCommand(
                        selectQuery,
                        con,
                        transaction
                    );


                    selectCmd.Parameters.AddWithValue(
                        "@UserID",
                        userID
                    );


                    SqlDataReader reader =
                        selectCmd.ExecuteReader();



                    if (reader.Read())
                    {
                        user = new LoginResult
                        {
                            UserID =
                            Convert.ToInt32(reader["UserID"]),

                            Username =
                            reader["Username"].ToString(),

                            Email =
                            reader["Email"].ToString(),

                            Phone =
                            reader["Phone"].ToString(),
                            RoleName =
                        reader["RoleName"].ToString()
                        };
                    }


                    reader.Close();



                    transaction.Commit();

                }
                catch
                {
                    transaction.Rollback();
                }

            }


            return user;
        }
        public static LoginResult GetUserByEmail(string email)
        {
            LoginResult user = null;


            using (SqlConnection con =
            new SqlConnection(DbConnection.StringConnection))
            {

                string query =
                @"
                SELECT 
            Users.UserID,
            Users.Username,
            Users.Email,
            Users.Phone,
            Users.PasswordHash,
            Role.RoleName,Users.IsVerified

        FROM Users

        INNER JOIN Role
        ON Users.RoleID = Role.RoleID

        WHERE Users.Email=@Email
                ";


                SqlCommand cmd =
                new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@Email",
                    email
                );


                con.Open();


                SqlDataReader reader =
                cmd.ExecuteReader();


                if (reader.Read())
                {
                    user = new LoginResult
                    {
                        UserID =
                        Convert.ToInt32(reader["UserID"]),

                        Username =
                        reader["Username"].ToString(),

                        Email =
                        reader["Email"].ToString(),

                        Phone =
                        reader["Phone"].ToString(),

                        PasswordHash =
                        reader["PasswordHash"].ToString(),
                        RoleName =
                        reader["RoleName"].ToString() ,
                        IsVerified =
Convert.ToBoolean(reader["IsVerified"])
                    };
                }

            }

            return user;
        }


        public LoginResult GetUserById(int userID)
        {

            LoginResult user = null;


            using (SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection))
            {

                string query =
                @"
        SELECT 
            UserID,
            Username,
            Email,
            Phone,
            RoleName

        FROM Users
        INNER JOIN Role
        ON Users.RoleID = Role.RoleID

        WHERE UserID=@UserID
        ";


                SqlCommand cmd =
                new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@UserID",
                    userID
                );


                con.Open();


                SqlDataReader reader =
                cmd.ExecuteReader();


                if (reader.Read())
                {
                    user = new LoginResult
                    {
                        UserID =
                        Convert.ToInt32(reader["UserID"]),

                        Username =
                        reader["Username"].ToString(),

                        Email =
                        reader["Email"].ToString(),

                        Phone =
                        reader["Phone"].ToString(),

                        RoleName =
                        reader["RoleName"].ToString()
                    };
                }

            }


            return user;
        }

        public LoginResult GetUserByEmail2(string email)
        {
            LoginResult user = null;


            using (SqlConnection con =
                new SqlConnection(DbConnection.StringConnection))
            {

                string query =
                @"
        SELECT 
            UserID,
            Username,
            Email,
            Phone,
            RoleID

        FROM Users

        WHERE Email=@Email
        ";


                SqlCommand cmd =
                new SqlCommand(query, con);


                cmd.Parameters.AddWithValue(
                    "@Email",
                    email
                );


                con.Open();


                SqlDataReader reader =
                cmd.ExecuteReader();



                if (reader.Read())
                {

                    user = new LoginResult
                    {

                        UserID =
                        Convert.ToInt32(reader["UserID"]),

                        Username =
                        reader["Username"].ToString(),

                        Email =
                        reader["Email"].ToString(),

                        Phone =
                        reader["Phone"].ToString(),

                    };

                }

            }


            return user;
        }

        public bool UpdatePassword(
int userID,
string password)
        {

            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);



            string query =
            @"
UPDATE Users
SET PasswordHash=@Password
WHERE UserID=@UserID
";



            SqlCommand cmd =
            new SqlCommand(query, con);


            cmd.Parameters.AddWithValue(
            "@Password",
            password);


            cmd.Parameters.AddWithValue(
            "@UserID",
            userID);



            con.Open();


            return cmd.ExecuteNonQuery() > 0;

        }

        public bool VerifyUser(int userID)
        {

            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            string query =
            @"
    UPDATE Users
    SET IsVerified = 1
    WHERE UserID = @UserID
    ";


            SqlCommand cmd =
            new SqlCommand(query, con);


            cmd.Parameters.AddWithValue(
            "@UserID",
            userID);


            con.Open();


            int rows =
            cmd.ExecuteNonQuery();


            return rows > 0;
        }
    }
}
