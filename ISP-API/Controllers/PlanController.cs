using ISP_API.Dtos.PlanDTOs;
using ISP_API.Services.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ISP_API.Controllers;

[ApiController]
[Route("api/planes")]
public class PlanController : ControllerBase
{
    private readonly IPlanService _planService;
    public PlanController(IPlanService planService)
    {
        _planService = planService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlan([FromBody] PlanCreateDto model)
    {
        var response = await _planService.CreatePlanAsync(model);
        if (response.Status)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlans()
    {
        var response = await _planService.GetAllPlansAsync();
        if (response.Status)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlanById(Guid id)
    {
        var response = await _planService.GetPlanByIdAsync(id);
        if (response.Status)
        {
            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlan(Guid id, [FromBody] PlanDto model)
    {
        model.Id = id;
        var response = await _planService.UpdatePlanAsync(model);
        if (response.Status)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlan(Guid id)
    {
        var response = await _planService.DeletePlanAsync(id);
        if (response.Status)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }
    
}