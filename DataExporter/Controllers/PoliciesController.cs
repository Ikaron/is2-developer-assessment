using DataExporter.Dtos;
using DataExporter.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataExporter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PoliciesController : ControllerBase
    {
        private PolicyService _policyService;

        public PoliciesController(PolicyService policyService) 
        { 
            _policyService = policyService;
        }

        [HttpPost]
        public async Task<ActionResult<ReadPolicyDto>> PostPolicies([FromBody]CreatePolicyDto createPolicyDto)
        {
            var policy = await _policyService.CreatePolicyAsync(createPolicyDto);
            if (policy == null)
            {
                return BadRequest("Failed to create policy.");
            }

            return Ok(policy);
        }

        [HttpGet]
        public async Task<ActionResult<IList<ReadPolicyDto>>> GetPolicies()
        {
            return Ok(await _policyService.ReadPoliciesAsync());
        }

        [HttpGet("{policyId}")]
        public async Task<ActionResult<ReadPolicyDto>> GetPolicy(int policyId)
        {
            var policy = await _policyService.ReadPolicyAsync(policyId);
            if (policy == null)
            {
                return NotFound();
            }

            return Ok(policy);
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportData([FromQuery]DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }
    }
}
