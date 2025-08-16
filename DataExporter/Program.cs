using DataExporter.Services;
using DataExporter.Dtos.Validators;
using FluentValidation;

namespace DataExporter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ExporterDbContext>();
            builder.Services.AddScoped<PolicyService>();
            builder.Services.AddScoped<ExportService>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreatePolicyDtoValidator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
