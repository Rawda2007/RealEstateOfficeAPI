using RealEstateOfficeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealStateOfficeModels.Properties;
namespace RealEstateOfficeBusinessLogic
{
    public class PurposeBL
    {
        
        public static List<Purpose> GetAll()
        {
            return PurposeDA.GetAll();
        }
    }
}
