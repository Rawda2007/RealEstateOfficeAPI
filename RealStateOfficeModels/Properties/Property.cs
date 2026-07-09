using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateOfficeModels.Properties
{
    public class Property
    {
        public int PropertyID { get; set; }


        public int OwnerID { get; set; }

        public int AreaID { get; set; }

        public int PurposeID { get; set; }

        public int StatusID { get; set; }


        public string Title { get; set; }

        public string Description { get; set; }


        public decimal Price { get; set; }

        public decimal AreaSize { get; set; }


        public int NumRooms { get; set; }

        public int NumBathrooms { get; set; }
    }
}
