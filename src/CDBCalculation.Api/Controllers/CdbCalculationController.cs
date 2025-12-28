using CDBCalculation.Application.DTOs;
using CDBCalculation.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CDBCalculation.Api.Controllers;

[ApiController]
[Route("api/cdb-calculation")]
public class CdbCalculationController : ControllerBase
{
    private readonly ICdbCalculationAppService _calculationAppService;


    public CdbCalculationController(
    ICdbCalculationAppService calculationAppService)
    {
        _calculationAppService = calculationAppService;
    }

    
    [HttpPost("calculate")]
    public async Task<IActionResult> Calculate(
       [FromBody] CdbCalculationRequestDto requestDto)
    {
        var result = await _calculationAppService
            .DoCDBCalculation(requestDto);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }


}
