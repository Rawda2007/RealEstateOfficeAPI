using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateOfficeBusinessLogic;
using RealEstateOfficeDataAccess;
using RealStateOfficeModels.Properties;
using static System.Net.Mime.MediaTypeNames;
namespace RealEstateOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly PropertyBL propertyBL = new PropertyBL(); 
        
        [HttpGet("All")]
        public IActionResult GetAll()
        {
            return Ok(
            propertyBL.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var property =
            propertyBL.GetByID(id);
            PropertyImageBL propertyImageBL = new();
           List<PropertyImageModel>? p= propertyImageBL.GetByPropertyID(property.PropertyID);
           foreach (var item in p)
            {
                property.Images.Add(item.ImagePath);
            }

            if (property == null)
            {
                return NotFound();
            }


            return Ok(property);
        }



        [HttpGet("ByGovernorate/{id}")]
        public IActionResult GetByGovernorate(int id)
        {
            return Ok(
                propertyBL.GetByGovernorate(id)
            );
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(PropertyModel model)
        {
            bool result =
            propertyBL.Add(model);

            if (!result)
            {
                return BadRequest();
            }

            return Ok("Property Added Successfully");
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(
int id,
PropertyModel model)
        {
            model.PropertyID = id;

            bool result =
            propertyBL.Update(model);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(
            "Property Updated Successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            PropertyImageBL propertyImageBL= new PropertyImageBL();
            var images =
propertyImageBL.GetByPropertyID(id);

foreach(var image in images)
{
    string filePath =
    Path.Combine(
    Directory.GetCurrentDirectory(),
    "wwwroot",
    image.ImagePath);

    if(System.IO.File.Exists(filePath))
    {
        System.IO.File.Delete(filePath);
    }
                propertyImageBL.Delete(image.ImageID);

            }


            bool result =
            propertyBL.Delete(id);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(
            "Property Deleted Successfully");
        }
    }
}
