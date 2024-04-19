using FileStorage;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.
builder.Services.AddSingleton<IFileUploadAdapter, MongoFileUploadAdapter>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(b => 
        b.SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo()
            {
                Title = "My API - V1",
                Version = "v1"
            }
        );

        var filePath = Path.Combine(System.AppContext.BaseDirectory, "FileStorage.xml");
        c.IncludeXmlComments(filePath);
    } );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
