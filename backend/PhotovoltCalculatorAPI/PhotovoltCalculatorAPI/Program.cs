using PhotovoltCalculatorAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureAuthServices();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddControllers();
builder.Services.ConfigureEmailService(builder.Configuration);
builder.Services.ConfigureAutomapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhotovoltCalculator");
});
app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
