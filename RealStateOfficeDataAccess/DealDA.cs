using Microsoft.Data.SqlClient;
using RealStateOfficeModels.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeDataAccess
{
   public class DealDA
    {
        public static bool IsPropertyExists(int propertyID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
SELECT 1
FROM Property
WHERE PropertyID = @PropertyID
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@PropertyID",
                propertyID);

            con.Open();

            object? result = cmd.ExecuteScalar();

            return result != null;
        }


        public static bool IsClientExists(int clientID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"SELECT 1
      FROM Client
      WHERE ClientID=@ClientID";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ClientID", clientID);

            con.Open();

            return cmd.ExecuteScalar() != null;
        }


        public static bool IsPropertyAvailable(int propertyID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
SELECT 1
FROM Property
WHERE PropertyID=@PropertyID
AND StatusID=1
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@PropertyID", propertyID);

            con.Open();

            return cmd.ExecuteScalar() != null;
        }


        public static bool HasApprovedDeal(int propertyID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
SELECT 1
FROM Deal
WHERE PropertyID=@PropertyID
AND StatusID=2
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@PropertyID", propertyID);

            con.Open();

            return cmd.ExecuteScalar() != null;
        }


        public static bool HasPendingDealForSameClient
(
    int propertyID,
    int clientID
)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
SELECT 1
FROM Deal
WHERE PropertyID=@PropertyID
AND ClientID=@ClientID
AND StatusID=1
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@PropertyID", propertyID);
            cmd.Parameters.AddWithValue("@ClientID", clientID);

            con.Open();

            return cmd.ExecuteScalar() != null;
        }


        public static bool Add(DealModel deal)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
INSERT INTO Deal
(
    PropertyID,
    ClientID,
    DealDate,
    Amount,
    StatusID
)
VALUES
(
    @PropertyID,
    @ClientID,
    @DealDate,
    @Amount,
    @StatusID
)";
            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@PropertyID", deal.PropertyID);
            cmd.Parameters.AddWithValue("@ClientID", deal.ClientID);
            cmd.Parameters.AddWithValue("@DealDate", deal.DealDate);
            cmd.Parameters.AddWithValue("@Amount", deal.Amount);
            cmd.Parameters.AddWithValue("@StatusID", deal.StatusID);

            con.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


        public static List<DealDetailsModel> GetAll()
        {
            List<DealDetailsModel> deals = new();

            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
SELECT

D.DealID,

P.Title,

U.Username AS UserName,

D.DealDate,

D.Amount,

DS.Name AS Status

FROM Deal D

INNER JOIN Property P
ON D.PropertyID = P.PropertyID

INNER JOIN Client C
ON D.ClientID = C.ClientID

inner join Users U
on c.UserID=u.UserID

INNER JOIN DealStatus DS
ON D.StatusID = DS.StatusID
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            con.Open();

            SqlDataReader reader =
                cmd.ExecuteReader();

            while (reader.Read())
            {
                deals.Add(new DealDetailsModel
                {
                    DealID = Convert.ToInt32(reader["DealID"]),
                    PropertyTitle = reader["Title"].ToString()!,
                    ClientName = reader["UserName"].ToString()!,
                    DealDate = Convert.ToDateTime(reader["DealDate"]),
                    Amount = Convert.ToDecimal(reader["Amount"]),
                    Status = reader["Status"].ToString()!
                });
            }

            return deals;
        }
        public static List<DealDetailModel> GetByClientID(int clientID)
        {
            List<DealDetailModel> deals = new();
            using SqlConnection con =
    new SqlConnection(DbConnection.StringConnection);

            string query = @"SELECT

D.DealID,

P.Title,

D.DealDate,

D.Amount,

DS.Name AS Status

FROM Deal D

INNER JOIN Property P
ON D.PropertyID = P.PropertyID

INNER JOIN DealStatus DS
ON D.StatusID = DS.StatusID

WHERE D.ClientID=@ClientID";

            SqlCommand cmd =
              new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DealID", clientID);

            con.Open();

            SqlDataReader reader =
              cmd.ExecuteReader();

            while (reader.Read())
            {
                deals.Add(new DealDetailModel
                {
                    DealID = Convert.ToInt32(reader["DealID"]),
                    PropertyTitle = reader["Title"].ToString()!,
                    DealDate = Convert.ToDateTime(reader["DealDate"]),
                    Amount = Convert.ToDecimal(reader["Amount"]),
                    Status = reader["Status"].ToString()!
                });
            }

            return null;
        }

        public static DealDetailsModel? GetByID(int dealID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
SELECT

D.DealID,

P.Title,

U.Username AS UserName,

D.DealDate,

D.Amount,

DS.Name AS Status

FROM Deal D

INNER JOIN Property P
ON D.PropertyID = P.PropertyID

INNER JOIN Client C
ON D.ClientID = C.ClientID

inner join Users U
on c.UserID=u.UserID

INNER JOIN DealStatus DS
ON D.StatusID = DS.StatusID

WHERE D.DealID=@DealID
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DealID", dealID);

            con.Open();

            SqlDataReader reader =
                cmd.ExecuteReader();

            if (reader.Read())
            {
                return new DealDetailsModel
                {
                    DealID = Convert.ToInt32(reader["DealID"]),
                    PropertyTitle = reader["Title"].ToString()!,
                    ClientName = reader["UserName"].ToString()!,
                    DealDate = Convert.ToDateTime(reader["DealDate"]),
                    Amount = Convert.ToDecimal(reader["Amount"]),
                    Status = reader["Status"].ToString()!
                };
            }

            return null;
        }


        public static bool Approve(int dealID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
UPDATE Deal
SET StatusID = 2
WHERE DealID=@DealID
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DealID", dealID);

            con.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


        public static bool Reject(int dealID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
UPDATE Deal
SET StatusID = 3
WHERE DealID=@DealID
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DealID", dealID);

            con.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


        public static DealModel? GetDeal(int dealID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
SELECT *
FROM Deal
WHERE DealID=@DealID
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DealID", dealID);

            con.Open();

            SqlDataReader reader =
                cmd.ExecuteReader();

            if (reader.Read())
            {
                return new DealModel
                {
                    DealID = Convert.ToInt32(reader["DealID"]),
                    PropertyID = Convert.ToInt32(reader["PropertyID"]),
                    ClientID = Convert.ToInt32(reader["ClientID"]),
                    DealDate = Convert.ToDateTime(reader["DealDate"]),
                    Amount = Convert.ToDecimal(reader["Amount"]),
                    StatusID = Convert.ToInt32(reader["StatusID"])
                };
            }

            return null;
        }

        public static bool RejectPendingDeals(int propertyID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
UPDATE Deal

SET StatusID=3

WHERE PropertyID=@PropertyID

AND StatusID=1
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@PropertyID",
                propertyID);

            con.Open();

            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int dealID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
DELETE FROM Deal
WHERE DealID=@DealID
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DealID", dealID);

            con.Open();

            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool IsPending(int dealID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
SELECT 1
FROM Deal
WHERE DealID=@DealID
AND StatusID=1
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DealID", dealID);

            con.Open();

            return cmd.ExecuteScalar() != null;
        }


        public static bool Cancel(
    int dealID,
    int clientID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
UPDATE Deal

SET StatusID = 4

WHERE DealID=@DealID

AND ClientID=@ClientID

AND StatusID=1
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DealID", dealID);
            cmd.Parameters.AddWithValue("@ClientID", clientID);

            con.Open();

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
