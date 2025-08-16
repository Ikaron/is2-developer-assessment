using DataExporter.Dtos;
using DataExporter.Model;
using FluentValidation;
using Microsoft.EntityFrameworkCore;


namespace DataExporter.Services
{
    public class PolicyService
    {
        private ExporterDbContext _dbContext;
        private IValidator<CreatePolicyDto> _validator;

        public PolicyService(ExporterDbContext dbContext, IValidator<CreatePolicyDto> validator)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();

            _validator = validator;
        }

        /// <summary>
        /// Creates a new policy from the DTO.
        /// </summary>
        /// <param name="createPolicyDto"></param>
        /// <returns>Returns a ReadPolicyDto representing the new policy, if succeded. Returns null, otherwise.</returns>
        public async Task<ReadPolicyDto?> CreatePolicyAsync(CreatePolicyDto createPolicyDto)
        {
            var result = await _validator.ValidateAsync(createPolicyDto);

            if (!result.IsValid)
                return null;

            var newPolicy = new Policy
            {
                PolicyNumber = createPolicyDto.PolicyNumber,
                Premium = createPolicyDto.Premium!.Value,
                StartDate = createPolicyDto.StartDate!.Value
            };

            _dbContext.Policies.Add(newPolicy);

            try
            {
                // We could process return value here, but SaveChangesAsync is guaranteed to throw if it fails.
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }

            return new ReadPolicyDto()
            {
                Id = newPolicy.Id,
                PolicyNumber = newPolicy.PolicyNumber,
                Premium = newPolicy.Premium,
                StartDate = newPolicy.StartDate
            };
        }

        /// <summary>
        /// Retrieves all policies.
        /// </summary>
        /// <returns>Returns a list of ReadPoliciesDto.</returns>
        public async Task<IList<ReadPolicyDto>> ReadPoliciesAsync()
        {
            // This was a dummy method before
            return await _dbContext.Policies
                .Select(policy => new ReadPolicyDto
                {
                    Id = policy.Id,
                    PolicyNumber = policy.PolicyNumber,
                    Premium = policy.Premium,
                    StartDate = policy.StartDate
                })
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a policy by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a ReadPolicyDto if a policy with this id exists. Returns null, otherwise.</returns>
        public async Task<ReadPolicyDto?> ReadPolicyAsync(int id)
        {
            // SingleAsync errors if not found, so we can use FirstOrDefaultAsync but because we query on primary key, FindAsync is better
            var policy = await _dbContext.Policies.FindAsync(id);
            if (policy == null)
            {
                return null;
            }

            var policyDto = new ReadPolicyDto()
            {
                Id = policy.Id,
                PolicyNumber = policy.PolicyNumber,
                Premium = policy.Premium,
                StartDate = policy.StartDate
            };

            return policyDto;
        }
    }
}
