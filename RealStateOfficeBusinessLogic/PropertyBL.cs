using RealStateOfficeModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateOfficeDataAccess;

namespace RealEstateOfficeBusinessLogic
{
    public class PropertyBL
    {
        private readonly PropertyDA da=new PropertyDA();
        public List<PropertyDetailsModel> GetAll()
        {
            return da.GetAll();
        }

        public PropertyDetailsWithImagesModel? GetByID(int propertyID)
        {
            return da.GetByID(propertyID);
        }

        public List<PropertyDetailsModel> GetByGovernorate(int governorateID)
        {
            return da.GetByGovernorate(governorateID);
        }

        public bool Add(PropertyModel model)
        {
            return da.Add(model);
        }

        public bool Update(PropertyModel model)
        {
            return da.Update(model);
        }

        public bool Delete(int propertyID)
        {
            return da.Delete(propertyID);
        }

        public static bool IsExist(int propertyID)
        {
            return PropertyDA.IsExist(propertyID);
        }

        public bool ChangeStatus(int propertyID, int statusID)
        {
            return PropertyDA.ChangeStatus(propertyID, statusID);
        }
    }
}
