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
        private ExportService _exportService;

        public PoliciesController(PolicyService policyService, ExportService exportService) 
        { 
            _policyService = policyService;
            _exportService = exportService;
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

        [HttpGet("export")]
        public async Task<ActionResult<IList<ExportDto>>> ExportData([FromQuery]DateTime startDate, [FromQuery]DateTime endDate)
        {
            var data = await _exportService.ExportAsync(startDate, endDate);
            if (data == null)
            {
                return BadRequest();
            }

            return Ok(data);
        }
    }
}
