namespace RealEstateOfficeAPI.Models
{
   
        public class PropertyImageModel
        {
            public int PropertyID { get; set; }

        public List<IFormFile> Images { get; set; } = new();

    }

}
