using AssestManagement.DTO;
using AssestManagement.Interfaces;
using AssestManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssestManagement.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetRepository _assetRepository;
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(IAssetRepository assetRepository, ILogger<AssetsController> logger)
        {
            _assetRepository = assetRepository;
            _logger = logger;
        }

        // GET: api/Assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            try
            {
                var assets = await _assetRepository.GetAssets();
                return Ok(assets);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting assets: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Assets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(int id)
        {
            try
            {
                var asset = await _assetRepository.GetAssetById(id);

                if (asset == null)
                {
                    return NotFound();
                }

                return Ok(asset);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting asset with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Assets
        [HttpPost]
        public async Task<ActionResult<int>> AddAsset(Asset asset/*, AssetImage assetImage, Document document*/)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int assetId = await _assetRepository.AddAsset(asset);
                
                return Ok($"AssetID {assetId} Added");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding asset: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Assets/5
        [HttpPut]
        public async Task<IActionResult> UpdateAsset( Asset asset)
        {
            try
            {
                
                await _assetRepository.UpdateAsset(asset);
                return Ok("Asset Details Updated");
              
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating asset: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

      //  POST: api/Assets/BulkUpdate
      
        [HttpPost("bulkUpdate")]
        public async Task<IActionResult> BulkUpdateAssets([FromBody] IEnumerable<AssetBulkUpdateRequest> request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _assetRepository.BulkUpdateAssets(request);              

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating assets: {ex.Message}");
                return StatusCode(500, "Internal server error");



            }
        }



        [HttpPost("addImage")]
        public async Task<IActionResult> AddAssetImage([FromBody] AssetImageDTO assetImageDTO )
        {
            try
            {
                byte[] imageContent = System.IO.File.ReadAllBytes(assetImageDTO.ImagePath);
                string base64String = Convert.ToBase64String(imageContent);

                var newAssetImage = new AssetImage
                {
                    AssetId = assetImageDTO.AssetId,
                    Name = assetImageDTO.ImageName,
                    Content = base64String

                };
                await _assetRepository.AddAssetImage(newAssetImage);
                return Ok("Asset Image Added");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Adding Asset: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpPut("updateAssetImage")]
        public async Task<IActionResult> UpdateAssetImage([FromBody] AssetImageDTO assetImageDTO)
        {
            try
            {

                byte[] imageContent = System.IO.File.ReadAllBytes(assetImageDTO.ImagePath);
                string base64String = Convert.ToBase64String(imageContent);

                var newAssetImage = new AssetImage
                {   
                    AssetImageId = assetImageDTO.AssetImageId,
                    AssetId = assetImageDTO.AssetId,
                    Name = assetImageDTO.ImageName,
                    Content = base64String

                };
                await _assetRepository.UpdateAssetImage(newAssetImage);
                return Ok("Asset Image Updated");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating asset: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }


        [HttpPost("addDocument")]
        public async Task<IActionResult> AddAssetDocument([FromBody] DocumentDTO documentDTO)
        {
            try
            {
                {
                    string base64String = documentDTO.DocumentPath;


                    var newDocument = new Document
                    {
                        AssetId = documentDTO.AssetId,
                        DocumentTitle = documentDTO.DocumentTitle,
                        Description = documentDTO.Description,
                        FileName = documentDTO.FileName,
                        FileExtension = documentDTO.FileExtension,
                        Content = base64String
                    };
                    await _assetRepository.AddAssetDocument(newDocument);
                    return Ok("Asset Document Added");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Adding Document: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("updateDocument")]
        public async Task<IActionResult> UpdateAssetDocument([FromBody] DocumentDTO documentDTO)
        {
            try
            {
                {

                    var newDocument = new Document
                    {
                        DocumentId = documentDTO.DocumentId,
                        AssetId = documentDTO.AssetId,
                        DocumentTitle = documentDTO.DocumentTitle,
                        Description = documentDTO.Description,
                        FileName = documentDTO.FileName,
                        FileExtension = documentDTO.FileExtension,
                        Content = documentDTO.DocumentPath
                    };
                    await _assetRepository.UpdateAssetDocument(newDocument);
                    return Ok("Asset Document Updated");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Updating Document: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

