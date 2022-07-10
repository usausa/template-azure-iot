using System.Data;

using Microsoft.Data.SqlClient;

using MudBlazor.Services;

using Smart.Data;
using Smart.Data.Accessor.Extensions.DependencyInjection;

using Template.Services;

#pragma warning disable CA1812

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

var connectionString = builder.Configuration.GetConnectionString("SQL");
builder.Services.AddSingleton<IDbProvider>(new DelegateDbProvider(() => new SqlConnection(connectionString)));
builder.Services.AddDataAccessor(c =>
{
    c.EngineOption.ConfigureTypeMap(map =>
    {
        map[typeof(DateTime)] = DbType.DateTime2;
    });
});

builder.Services.AddSingleton<SensorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
