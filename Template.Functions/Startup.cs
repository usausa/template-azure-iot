namespace Template.Functions;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

using Smart.Data;
using Smart.Data.Accessor.Extensions.DependencyInjection;

using Template.Services;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        // Data
        var connectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
        builder.Services.AddSingleton<IDbProvider>(new DelegateDbProvider(() => new SqlConnection(connectionString)));

        builder.Services.AddDataAccessor(c =>
        {
            c.EngineOption.ConfigureTypeMap(map =>
            {
                map[typeof(DateTime)] = DbType.DateTime2;
            });
        });

        // Service
        builder.Services.AddSingleton<SensorService>();
    }
}
