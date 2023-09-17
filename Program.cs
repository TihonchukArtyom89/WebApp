using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
//using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
builder.Services.AddControllers();
builder.Services.Configure<MvcNewtonsoftJsonOptions>(opts =>
{
    opts.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});
builder.Services.Configure<MvcOptions>(opts => 
{
    opts.RespectBrowserAcceptHeader = true;
    opts.ReturnHttpNotAcceptable = true;
});
var app = builder.Build();
app.MapControllers();
//app.UseMiddleware<TestMiddleware>();
app.MapGet("/", () => "Hello World!");
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);
app.Run();


/*
//web service on endpoints
using Microsoft.EntityFrameworkCore;
using WebApp;
using WebApp.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(opts =>
{ 
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true); 
});
var app = builder.Build();
const string BASEURL = "api/products";
app.MapGet($"{BASEURL}/{{id}}",async (HttpContext context,DataContext data) => 
{
    string? id = context.Request.RouteValues["id"] as string;
    if(id!= null)
    {
        Product? p =data.Products.Find(long.Parse(id));
        if(p==null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        else
        {
            context.Response.ContentType="application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize<Product>(p));
        }
    }
});
app.MapGet(BASEURL,async (HttpContext context,DataContext data) =>
{
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(JsonSerializer.Serialize<IEnumerable<Product>>(data.Products));
});
app.MapPost(BASEURL,async (HttpContext context,DataContext data) => 
{
    Product? p = await JsonSerializer.DeserializeAsync<Product>(context.Request.Body);
    if(p!=null)
    {
        await data.AddAsync(p);
        await data.SaveChangesAsync();
        context.Response.StatusCode = StatusCodes.Status200OK;
    }
});
//app.UseMiddleware<TestMiddleware>();
app.MapGet("/", () => "Hello World!");
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);
app.Run();
*/