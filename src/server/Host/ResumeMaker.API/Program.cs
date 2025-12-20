using ResumeMaker.API.Extensions;
using ResumeMaker.API.Filters;

var builder = WebApplication.CreateBuilder(args);

var metadataOutputPath = Path.GetFullPath(
    Path.Combine(builder.Environment.ContentRootPath, "../../../client/src/metadata.ts"));

builder.Services.AddScoped<ServiceResultActionFilter>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ServiceResultActionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostServices(
    builder.Configuration,
    "http://localhost:5296/swagger/v1/swagger.json",
    metadataOutputPath);

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(x =>
        x.WithOrigins("http://localhost:5173", "http://localhost:4173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
    });
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseMetadataGenerator();
app.Run();
