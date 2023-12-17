namespace Client.DeviceClientExample;

using System.Text;
using System.Text.Json;

using Microsoft.Azure.Devices.Client;

public static class Program
{
    private const string ConnectionString =
        "HostName={hub}.azure-devices.net;DeviceId={deviceId};SharedAccessKey={ssa}";

    private static readonly Guid Id = Guid.Parse("00000000-0000-0000-0000-000000000000");

#pragma warning disable CA5394
    public static async Task Main()
    {
#pragma warning disable CA2007
        await using var client = DeviceClient.CreateFromConnectionString(ConnectionString, TransportType.Mqtt);
#pragma warning restore CA2007

        Console.WriteLine("Press control-C to exit.");
        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, ea) =>
        {
            ea.Cancel = true;
            // ReSharper disable once AccessToDisposedClosure
            cts.Cancel();
            Console.WriteLine("Exiting...");
        };

        var rand = new Random();
        while (!cts.IsCancellationRequested)
        {
            var value = new SensorValue
            {
                Id = Id,
                Value = rand.NextDouble() * 100
            };
            var json = JsonSerializer.Serialize(value);
            using var message = new Message(Encoding.UTF8.GetBytes(json));

            await client.SendEventAsync(message, cts.Token).ConfigureAwait(false);
            Console.WriteLine(".");

            await Task.Delay(60_000, cts.Token).ConfigureAwait(false);
        }
    }
#pragma warning restore CA5394
}

public sealed class SensorValue
{
    public Guid Id { get; set; }

    public double Value { get; set; }
}
