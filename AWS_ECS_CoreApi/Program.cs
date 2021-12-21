using System.Text.Json.Serialization;
using Amazon.S3;
using AWS_ECS_CoreApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();

builder.Services
    .AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}); })
    .AddControllers();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());


await builder.Services.AddAWSAccessCredentials();
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddControllers(setup => { setup.ReturnHttpNotAcceptable = true; }).AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    o.JsonSerializerOptions.MaxDepth = 500;
});

var app = builder.Build();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
app.MapHealthChecks("/health");
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.MapGet("/", () => "Hello World!");


app.Run();