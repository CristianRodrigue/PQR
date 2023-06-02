using ApiProcolombiaPQR.DATA;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var MyPolicy = "_myPolicy";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContextDB>(x => x.UseSqlServer("name=Conexion"));
builder.Services.AddTransient<SeedDB>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyPolicy,
                      policy =>
                      {
                          policy.WithOrigins("*").WithHeaders("*").WithMethods("*");
                      });
});

var app = builder.Build();
SeedDBInit(app);

void SeedDBInit(WebApplication app)
{
    IServiceScopeFactory? scopeFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopeFactory!.CreateScope())
    {
        SeedDB? service = scope.ServiceProvider.GetService<SeedDB>();
        service!.SeedAsync().Wait();
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyPolicy);

app.UseAuthorization();

/*app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
}); ;*/

app.MapControllers();

app.Run();
