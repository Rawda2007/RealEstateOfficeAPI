using RealEstateOfficeDataAccess;
using RealStateOfficeModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeBusinessLogic
{
    public class GovernorateBL
    {
        GovernorateDA da =
        new GovernorateDA();

        public List<GovernorateModel> GetAll()
        {
            return da.GetAll();
        }
    }
}
