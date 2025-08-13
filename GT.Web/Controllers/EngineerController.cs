using GT.Application.Common;
using GT.Application.Services.Engineers;
using GT.Application.Services.Engineers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GT.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EngineerController(IEngineerService engineerService, IValidationHelper _validator) : ControllerBase
{
    /// <summary>
    /// Retrieves all allocation statuses.
    /// </summary>
    /// <returns>A list of allocation statuses.</returns>
    [HttpGet("get-all-engineers")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await engineerService.GetAllEngineersAsync());
    }

    /// <summary>
    /// Retrieves all allocation statuses.
    /// </summary>
    /// <returns>A list of allocation statuses.</returns>
    [HttpPost("insert-engineer")]
    public async Task<IActionResult> InsertEngineer(CreateEngineerDto engineerDto)
    {
        await _validator.ValidateAsync(engineerDto);
        return Ok(await engineerService.InsertEngineerAsync(engineerDto));
    }
}
