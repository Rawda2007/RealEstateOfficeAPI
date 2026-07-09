using RealStateOfficeModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateOfficeDataAccess;
namespace RealEstateOfficeBusinessLogic
{
    public class PropertyStatusBL
    {
        PropertyStatusDA da =
        new PropertyStatusDA();

        public List<PropertyStatus> GetAll()
        {
            return da.GetAll();
        }
    }
}
