using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateOfficeAPI.Models;
using RealEstateOfficeBusinessLogic;
using RealEstateOfficeDataAccess;
using RealStateOfficeModels.Properties;

namespace RealEstateOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImageController : ControllerBase
    {
        private readonly PropertyImageBL propertyImageBL =
new PropertyImageBL();

        [Authorize(Roles = "Admin")]
        [HttpPost("UploadImages/{propertyId}")]
        public IActionResult UploadImages(
      int propertyId,
      [FromForm] List<IFormFile> images)
        
            {
            if (!PropertyBL.IsExist(propertyId))
            {
                return NotFound("Property not found.");
            }

            if (images == null || images.Count == 0)
            {
                return BadRequest("Please select at least one image.");
            }

            string[] allowedExtensions =
                {
        ".jpg",
        ".jpeg",
        ".png"
    };
            foreach (var image in images)
                {
                // 1- التأكد من امتداد الصورة

                string extension =
                Path.GetExtension(image.FileName).ToLower();

                

                if (!allowedExtensions.Contains(extension))
                {
                    return BadRequest(
                        $"{image.FileName} is not a valid image."
                    );
                }

                // 2- Generate File Name
                string fileName =
                    Guid.NewGuid().ToString()
                    +
                    Path.GetExtension(image.FileName);


                    string folderPath =
                    Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "PropertyImages");


                    string filePath =
                    Path.Combine(
                    folderPath,
                    fileName);


                    using FileStream stream =
                    new FileStream(
                    filePath,
                    FileMode.Create);


                    image.CopyTo(stream);


                    PropertyImageDataModel propertyImage =
                    new PropertyImageDataModel()
                    {
                        PropertyID = propertyId,

                        ImagePath =
                        $"PropertyImages/{fileName}"
                    };


                    propertyImageBL.Add(propertyImage);
                }

                return Ok("Images Uploaded Successfully");
            }

        [HttpGet("{propertyId}")]
        public IActionResult GetImages(int propertyId)
        {
            if (!PropertyBL.IsExist(propertyId))
            {
                return NotFound("Property Not Found");
            }

            var images =
            propertyImageBL.GetByPropertyID(propertyId);

            foreach (var image in images)
            {
                image.ImagePath =
                $"{Request.Scheme}://{Request.Host}/{image.ImagePath}";
            }

            return Ok(images);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{imageId}")]
        public IActionResult Delete(int imageId)
        {
            var image =
            propertyImageBL.GetByID(imageId);

            if (image == null)
            {
                return NotFound("Image not found.");
            }

            string filePath =
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                image.ImagePath);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            propertyImageBL.Delete(imageId);

            return Ok("Image deleted successfully.");
        }
    }
 }

