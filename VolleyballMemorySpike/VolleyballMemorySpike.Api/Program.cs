using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowBlazorOrigin",
                      builder =>
                      {
                          //TODO: Restirct only to client https
                          builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});

// Database Configuration
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
{
    var dbConnString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(dbConnString);
});

// MediatR Configuration
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(Program).Assembly));

// DI
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<VolleyballMemorySpike.Authentication.AuthenticationService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazorOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
