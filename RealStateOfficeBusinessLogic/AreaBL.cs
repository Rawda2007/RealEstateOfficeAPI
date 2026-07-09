using RealEstateOfficeDataAccess;
using RealStateOfficeModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeBusinessLogic
{
    public class AreaBL
    {
        public AreaDA areaDA = new AreaDA();

        public List<AreaModel> GetAll()
        {
            return areaDA.GetAll();
        }
        public List<AreaModel> GetByGovernorate(int governorateID)
        {
            return areaDA.GetByGovernorate(governorateID);
        }
    }
}
