using Accounting_APIS.Models;
using Accounting_APIS.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // Add this for Swagger
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin() // Allow any origin
                   .AllowAnyMethod() // Allow any method (GET, POST, etc.)
                   .AllowAnyHeader(); // Allow any header
        });
});

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Use original property names
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // Ignore null values
    });

// Configure Entity Framework Core
builder.Services.AddDbContext<AccountingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AccountingDb")));

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IFinancialReportRepository, FinancialReportRepository>();
builder.Services.AddScoped<IJournalEntryRepository, JournalEntryRepository>();
builder.Services.AddScoped<ITrialBalanceRepository, TrialBalanceRepository>();
builder.Services.AddScoped<ILedgerRepository, LedgerRepository>();
builder.Services.AddScoped<IJournalEntryDetailRepository, JournalEntryDetailRepository>();
builder.Services.AddScoped<IReportRequestRepository, ReportRequestRepository>();


// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ACCOUNTING APIs", Version = "v1" });
});

// Enable middleware to serve generated Swagger as a JSON endpoint



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable middleware to serve generated Swagger as a JSON endpoint.
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),


// Enable middleware to serve generated Swagger as a JSON endpoint
app.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ACCOUNTING API V1");
    c.RoutePrefix = "swagger"; // Set the route prefix for the Swagger UI
});


app.UseHttpsRedirection();

// Use CORS policy
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
