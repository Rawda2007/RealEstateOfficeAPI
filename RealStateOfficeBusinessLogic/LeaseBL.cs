using RealEstateOfficeDataAccess;
using RealStateOfficeModels.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeBusinessLogic
{
    public class LeaseBL
    {
        public bool Add(LeaseModel lease)
        {
            if (!LeaseDA.IsDealExists(lease.DealID))
                return false;

            if (!LeaseDA.IsApproved(lease.DealID))
                return false;

            if (LeaseDA.HasLease(lease.DealID))
                return false;

            if (lease.StartDate >= lease.EndDate)
                return false;

            if (lease.MonthlyRent <= 0)
                return false;

            return LeaseDA.Add(lease);
        }


        public List<LeaseDetailsModel> GetAll()
        {
            return LeaseDA.GetAll();
        }

        public LeaseDetailsModel? GetByID(int leaseID)
        {
            return LeaseDA.GetByID(leaseID);
        }

        public bool Update(LeaseModel lease)
        {
            if (lease.StartDate >= lease.EndDate)
                return false;

            if (lease.MonthlyRent <= 0)
                return false;

            return LeaseDA.Update(lease);
        }

        public bool Delete(int leaseID)
        {
            return LeaseDA.Delete(leaseID);
        }

        public List<LeaseDetailsModel> GetMyLeases(int clientID)
        {
            return LeaseDA.GetByClientID(clientID);
        }

        public LeaseDetailsModel? GetMyLease(
    int leaseID,
    int clientID)
        {
            return LeaseDA.GetByIDForClient(
                leaseID,
                clientID);
        }
    }
}
