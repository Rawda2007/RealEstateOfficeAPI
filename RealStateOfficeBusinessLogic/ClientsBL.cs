using RealEstateOfficeDataAccess;
using RealStateOfficeModels.DTOs;
using RealStateOfficeModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RealEstateOfficeBusinessLogic
{
    public class ClientsBL
    {
        public static List<Client> GetAll()
        {
            Clients clientsDA = new Clients();
            return clientsDA.GetAll();
        }

        public static Client GetClientByClientID(int id)
        {
            return Clients.GetClientByClientID(id);

        }

        public static bool AddClient(ClientDTO client)
        {
            return Clients.AddClient(client);
        }

        public static int GetClientIDByUserID(int userID)
        {
            return Clients.GetClientIDByUserID(userID);
        }
        public static int GetUserIDByClientID(int clientID)
        {
            return Clients.GetUserIDByClientID(clientID);
        }
        public static bool Update(int UserID, ClientDTO client)
        {
            return Clients.Update(UserID, client);
        }

        public static int Delete(int ClientID)
        {
            return Clients.Delete(ClientID);
        }

        public static List<Client> Search(string UserName)
        {
            return Clients.Search(UserName);
        }
    }
}
