using DataExporter.Dtos;
using DataExporter.Model;
using Microsoft.EntityFrameworkCore;


namespace DataExporter.Services
{
    public class ExportService
    {
        private ExporterDbContext _dbContext;

        public ExportService(ExporterDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Retrieves all policies and their notes in a given date range
        /// </summary>
        /// <remarks
        /// </remarks>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Returns a list of ExportDto.</returns>
        public async Task<IList<ExportDto>> ExportAsync(DateTime startDate, DateTime endDate)
        {
            var result = await _dbContext.Policies
                .Where(policy => startDate <= policy.StartDate && policy.StartDate <= endDate)
                .Select(policy => new ExportDto
                {
                    PolicyNumber = policy.PolicyNumber,
                    Premium = policy.Premium,
                    StartDate = policy.StartDate,
                    Notes = policy.Notes.Select(note => note.Text).ToList()
                }).ToListAsync();

            return result;
        }
    }
}
