using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API_ASSIGNMENT.Data;
using Web_API_ASSIGNMENT.Model;

namespace Web_API_ASSIGNMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeController : ControllerBase
    {
        private WebApiDbContext _webApiDbContext;
        public BarcodeController(WebApiDbContext webApiDbContext)
        {
            _webApiDbContext = webApiDbContext;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Barcode barcode)
        
        {
            if(barcode == null)
            {
                return BadRequest();
            }
            _webApiDbContext.Barcodes.Add(barcode);
            await _webApiDbContext.SaveChangesAsync();
            return Ok(barcode);
        }

    }
}
