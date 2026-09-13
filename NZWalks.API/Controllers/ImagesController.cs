using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST : /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if(ModelState.IsValid)
            {
                //User repository to Upload image
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtention = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileDescription = request.FileDescription,
                    FileName = request.FileName
                };


                // User repository to upload image
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtention = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtention.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extention");
            }

            // 10485760 is equal to 10MB
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, Please upload smaller size");
            }
        }
    }
}
