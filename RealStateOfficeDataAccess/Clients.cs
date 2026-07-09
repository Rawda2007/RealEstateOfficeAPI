using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;

using System.Threading.Tasks;
using RealStateOfficeModels.Users;
using RealStateOfficeModels.DTOs;

namespace RealEstateOfficeDataAccess
{
    public class Clients
    {

       static string connectionString = DbConnection.StringConnection;

             // Get All Clients
        public List<Client> GetAll()
        {
            List<Client> clients = new List<Client>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select c.ClientID ,u.Username ,u.Email,u.Phone from Client c " +
                                "join Users U on U.UserID=c.UserID where U.IsVerified=1";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    Client client = new Client
                    {
                        ClientID = Convert.ToInt32(reader["ClientID"]),
                        Username = (reader["Username"]).ToString(),
                        Email =(reader["Email"]).ToString(),
                        Phone = (reader["Phone"]).ToString()
                    };

                    clients.Add(client);
                }
            }


            return clients;
        }


        // Get Client By ID
        public static Client GetClientByClientID(int id)
        {
            Client client = null;


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select c.ClientID ,u.Username ,u.Email,u.Phone from Client c " +
                                "join Users U on U.UserID=c.UserID WHERE ClientID=@id";


                SqlCommand cmd =
                new SqlCommand(query, con);


                cmd.Parameters.AddWithValue("@id", id);


                con.Open();


                SqlDataReader reader = cmd.ExecuteReader();



                if (reader.Read())
                {
                    client = new Client
                    {
                        ClientID = Convert.ToInt32(reader["ClientID"]),
                        Username = (reader["Username"]).ToString(),
                        Email = (reader["Email"]).ToString(),
                        Phone = (reader["Phone"]).ToString()
                    };
                }
            }


            return client;
        }

        public static bool AddClient( ClientDTO client)
        {
            using (SqlConnection con = new SqlConnection (connectionString))
            {
                con.Open();

                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // 1- Add User
                    string userQuery =
                    @"INSERT INTO Users
            (Username, PasswordHash, Email, Phone, RoleID)
            VALUES
            (@Username,@PasswordHash,@Email,@Phone,3);

            SELECT SCOPE_IDENTITY();";


                    SqlCommand userCmd = new SqlCommand(userQuery, con, transaction);


                    userCmd.Parameters.AddWithValue("@Username", client.Username);
                    userCmd.Parameters.AddWithValue("@PasswordHash", client.Password);
                    userCmd.Parameters.AddWithValue("@Email", client.Email);
                    userCmd.Parameters.AddWithValue("@Phone", client.Phone);



                    int userID = Convert.ToInt32(userCmd.ExecuteScalar());



                    // 2- Add Client
                    string clientQuery =
                    @"INSERT INTO Client
            (UserID, PurposeID)
            VALUES
            (@UserID,1)";


                    SqlCommand clientCmd =
                    new SqlCommand(clientQuery, con, transaction);


                    clientCmd.Parameters.AddWithValue("@UserID", userID);



                    clientCmd.ExecuteNonQuery();



                    transaction.Commit();

                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                return false;
            }
        }

        public static int GetClientIDByUserID(int userID)
        {
            int clientID = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query =
                @"SELECT ClientID
          FROM Client
          WHERE UserID = @UserID";


                SqlCommand cmd =
                new SqlCommand(query, con);


                cmd.Parameters.AddWithValue("@UserID", userID);


                con.Open();


                var result = cmd.ExecuteScalar();


                if (result != null)
                {
                    clientID = Convert.ToInt32(result);
                }
            }


            return clientID;
        }
        public static int GetUserIDByClientID(int clientID)
        {
            int userID = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query =
                @"SELECT UserID
          FROM Client
          WHERE clientID = @clientID";


                SqlCommand cmd =
                new SqlCommand(query, con);


                cmd.Parameters.AddWithValue("@clientID", clientID);


                con.Open();


                var result = cmd.ExecuteScalar();


                if (result != null)
                {
                    userID = Convert.ToInt32(result);
                }
            }


            return userID;
        }
        public static bool Update(int UserID,ClientDTO client)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                int clientID = GetClientIDByUserID(UserID);
                if (clientID==0 )
                {
                    return false;
                }
                    SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // Update User
                    string userQuery =
                    @"UPDATE Users
              SET Username=@Username,
                  Email=@Email,PasswordHash=@PasswordHash,
                  Phone=@Phone
              WHERE UserID=@UserID";


                    SqlCommand userCmd =
                    new SqlCommand(userQuery, con, transaction);


                    userCmd.Parameters.AddWithValue("@Username", client.Username);
                    userCmd.Parameters.AddWithValue("@Email", client.Email);
                    userCmd.Parameters.AddWithValue("@Phone", client.Phone);
                    userCmd.Parameters.AddWithValue("@PasswordHash", client.Password);
                    userCmd.Parameters.AddWithValue("@UserID", UserID);


                    userCmd.ExecuteNonQuery();



                    // Update Client
                    string clientQuery =
                    @"UPDATE Client
              SET PurposeID=@PurposeID
              WHERE ClientID=@ClientID";


                    SqlCommand clientCmd =
                    new SqlCommand(clientQuery, con, transaction);

                    
                    clientCmd.Parameters.AddWithValue("@PurposeID", "1");
                    clientCmd.Parameters.AddWithValue("@ClientID", clientID);


                    clientCmd.ExecuteNonQuery();



                    transaction.Commit();

                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static int Delete(int clientID)
        {
            if (clientID==0 ) {
                return 0;
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                int userID= GetUserIDByClientID(clientID);
                SqlTransaction transaction = con.BeginTransaction();

                try
                {

                    // Delete RefreshTokens first
                    SqlCommand refreshCmd =
                    new SqlCommand(
                    "DELETE FROM RefreshTokens WHERE UserID=@UserID",
                    con,
                    transaction);


                    refreshCmd.Parameters.AddWithValue(
                        "@UserID",
                        userID
                    );


                    refreshCmd.ExecuteNonQuery();

                    // Delete Client
                    SqlCommand clientCmd =
                    new SqlCommand(
                    "DELETE FROM Client WHERE ClientID=@ClientID",
                    con,
                    transaction);


                    clientCmd.Parameters.AddWithValue("@ClientID", clientID);

                    clientCmd.ExecuteNonQuery();



                    // Delete User
                    SqlCommand userCmd =
                    new SqlCommand(
                    "DELETE FROM Users WHERE UserID=@UserID",
                    con,
                    transaction);


                    userCmd.Parameters.AddWithValue("@UserID", userID);

                    userCmd.ExecuteNonQuery();



                    transaction.Commit();

                    return 1;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static List<Client> Search(string UserName)
        {
            List<Client> clients = new List<Client>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select c.ClientID ,u.Username ,u.Email,u.Phone from Client c " +
                                "join Users U on U.UserID=c.UserID WHERE u.Username LIKE @UserName";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserName", "%" + UserName + "%");
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Client client = new Client
                    {
                        ClientID = Convert.ToInt32(reader["ClientID"]),
                        Username = (reader["Username"]).ToString(),
                        Email = (reader["Email"]).ToString(),
                        Phone = (reader["Phone"]).ToString()
                    };

                    clients.Add(client);
                }
            }


            return clients;

        }

    }
}

