namespace Client.MqttClientExample;

using System.Text;
using System.Text.Json;

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;

public static class Program
{
    private const string Server = "{name}.azure-devices.net";
    private const string ClientId = "{deviceId}";
    private const string Username = $"{Server}/{ClientId}/api-version=2021-04-12";
    private const string Password = "{SharedAccessSignature ...}";
    private const string TopicD2C = $"devices/{ClientId}/messages/events/";
    //private const string TopicC2D = $"devices/{ClientId}/messages/devicebound/#";

    private static readonly Guid Id = Guid.Parse("00000000-0000-0000-0000-000000000000");

#pragma warning disable CA5394
    public static async Task Main()
    {
        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(Server, 8883)
            .WithClientId(ClientId)
            .WithCredentials(Username, Password)
            .WithProtocolVersion(MqttProtocolVersion.V311)
            .WithTls()
            .WithCleanSession()
            .Build();

        var factory = new MqttFactory();
        using var client = factory.CreateMqttClient();

        client.ConnectedAsync += args =>
        {
            Console.WriteLine("ConnectedAsync: " + args.ConnectResult);
            return Task.CompletedTask;
        };
        client.DisconnectedAsync += args =>
        {
            Console.WriteLine("DisconnectedAsync: " + args.ReasonString);
            return Task.CompletedTask;
        };
        client.ApplicationMessageReceivedAsync += args =>
        {
            Console.WriteLine("ApplicationMessageReceivedAsync: " + Encoding.UTF8.GetString(args.ApplicationMessage.Payload));
            return Task.CompletedTask;
        };

        await client.ConnectAsync(options).ConfigureAwait(false);

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
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(TopicD2C)
                .WithPayload(json)
                .Build();

            await client.PublishAsync(message, cts.Token).ConfigureAwait(false);
            Console.WriteLine(".");

            await Task.Delay(60_000, cts.Token).ConfigureAwait(false);
        }
    }
#pragma warning restore CA5394
}

public class SensorValue
{
    public Guid Id { get; set; }

    public double Value { get; set; }
}
