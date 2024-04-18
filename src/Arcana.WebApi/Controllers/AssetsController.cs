using Arcana.Domain.Entities.Commons;
using Arcana.Service.Configurations;
using Arcana.Service.Services.Assets;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Arcana.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssetsController(IAssetService assetService) : ControllerBase
{
    // GET: api/<AssetsController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<AssetsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<AssetsController>
    [HttpPost]
    public async Task<IActionResult> PostAsync(IFormFile file, FileType type)
    {
        return Ok(await assetService.UploadAsync(file, type));
    }

    // PUT api/<AssetsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<AssetsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
