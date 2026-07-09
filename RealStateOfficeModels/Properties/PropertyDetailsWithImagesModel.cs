using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateOfficeModels.Properties
{
    public class PropertyDetailsWithImagesModel
    {
        public int PropertyID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal AreaSize { get; set; }

        public int NumRooms { get; set; }

        public int NumBathrooms { get; set; }

        public string Area { get; set; }

        public string Governorate { get; set; }

        public string Purpose { get; set; }

        public string Status { get; set; }

        public List<string> Images { get; set; }
        = new();
    }
}
