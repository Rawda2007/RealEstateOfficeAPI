using RealEstateOfficeDataAccess;
using RealStateOfficeModels.Properties;
using RealStateOfficeModels.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeBusinessLogic
{
    public class DealBL
    {
        public List<DealDetailsModel> GetAll()

        {
            return DealDA.GetAll();
        }

        public DealDetailsModel? GetByID(int dealID)

        {
            return DealDA.GetByID(dealID);

        }
        public bool Add(DealModel deal)
        {
            if (!DealDA.IsPropertyExists(deal.PropertyID))
                return false;

            if (!DealDA.IsClientExists(deal.ClientID))
                return false;

            if (!DealDA.IsPropertyAvailable(deal.PropertyID))
                return false;

            if (DealDA.HasApprovedDeal(deal.PropertyID))
                return false;

            if (DealDA.HasPendingDealForSameClient(
                deal.PropertyID,
                deal.ClientID))
                return false;

            deal.StatusID = 1;

            deal.DealDate = DateTime.Now;

            return DealDA.Add(deal);
        }

        public bool Approve(int dealID)
        {
            DealModel? deal =
                DealDA.GetDeal(dealID);

            if (deal == null)
                return false;

            if (deal.StatusID != 1)
                return false;

            bool result =
                DealDA.Approve(dealID);

            if (!result)
                return false;

            PropertyBL propertyBL =
                new PropertyBL();

            var property =
                propertyBL.GetByID(deal.PropertyID);

            if (property == null)
                return false;

            if (property.Purpose == "Sale")
            {
                propertyBL.ChangeStatus(
                    property.PropertyID,
                    2); // Sold
            }
            else
            {
                propertyBL.ChangeStatus(
                    property.PropertyID,
                    3); // Rented
            }

            DealDA.RejectPendingDeals(
                deal.PropertyID);

            return true;
        }

        public bool Reject(int dealID)
        {
            return DealDA.Reject(dealID);
        }

        public bool Delete(int dealID)
        {
            return DealDA.Delete(dealID);
        }

        public List<DealDetailModel> GetMyDeals(int clientID)
        {
            return DealDA.GetByClientID(clientID);
        }

        public bool CancelDeal(
      int dealID,
      int clientID)
        {
            return DealDA.Cancel(
                dealID,
                clientID);
        }


    }
}
