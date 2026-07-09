using RealEstateOfficeDataAccess;
using RealStateOfficeModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeBusinessLogic
{
    public class PropertyImageBL
    {
        private readonly PropertyImageDA da =
        new PropertyImageDA();


        public bool Add(PropertyImageDataModel model)
        {
            return da.Add(model);
        }


        public List<PropertyImageModel> GetByPropertyID(int propertyID)
        {
            return PropertyImageDA.GetByPropertyID(propertyID);
        }

        public PropertyImageModel? GetByID(int imageID)
        {
            return da.GetByID(imageID);
        }

        public bool Delete(int imageID)
        {
            return da.Delete(imageID);
        }
    }
}
