using ResumeMaker.API.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddHostServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(opt =>
    {
        opt.RouteTemplate = "docs/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(x =>
    {
        x.DocumentTitle = "ResumeMaker";
        x.RoutePrefix = "docs";
        x.SwaggerEndpoint("/docs/v1/swagger.json", "My API v1");
    });
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
