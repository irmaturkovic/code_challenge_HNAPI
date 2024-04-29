using HackerNewsAPI.Core.Interfaces;
using HackerNewsAPI.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IHackerNewsService, HackerNewsService>();

builder.Services.AddHttpClient("HackerNewsAPI", client => {
    client.BaseAddress = new Uri(builder.Configuration["HackerNewsAPI:BaseURL"]);
});

builder.Services.AddMemoryCache();

builder.Services.AddCors
    (options => { options.AddPolicy("AllowLocalhost4200", 
        builder => { builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod(); 
        }); 
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost4200");

app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();

app.Run();
