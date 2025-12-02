using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Api_TiendaOnline.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Api_TiendaOnlineContext>(options =>
    //options.UseNpgsql(builder.Configuration.GetConnectionString("Api_Tienda.Postgres") ?? throw new InvalidOperationException("Connection string 'Api_TiendaOnlineContext' not found."))


    //options.UseMySql(builder.Configuration.GetConnectionString("Api_Tienda.mariadb") ?? throw new InvalidOperationException("Connection string 'Api_Tienda.mariadb' not found."),
    //Microsoft.EntityFrameworkCore.ServerVersion.Parse("12.0.2-MariaDB"))

    options.UseSqlServer(builder.Configuration.GetConnectionString("Api_Tienda.sqlserver") ?? throw new InvalidOperationException("Connection string 'Api_Tienda.sqlserver' not found."))



    );

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
