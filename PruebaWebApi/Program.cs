using BusinessLogic.Product.Interface;
using BusinessLogic.Product.Service;
using PruebaWebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API PRUEBA",
        Version = "v1"
    });
});


builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IProductCache, ProductCache>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddHttpClient<IProductService, ProductService>();

//configuracion de Cors
//builder.Services.AddCors(cors => cors.AddPolicy("corsapp", builder =>
//{
//    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
//}));

//builder.Services.AddCors(cors => cors.AddPolicy("corsapp", builder =>
//{
//    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
//}));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware
app.UseMiddleware<LoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

//app.UseCors("corsapp");
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
