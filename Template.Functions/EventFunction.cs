namespace Template.Functions;

using System.Text.Json;

using Azure.Messaging.EventHubs;

using Microsoft.Azure.WebJobs;

using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

public sealed class EventFunction
{
    private readonly ILogger<EventFunction> log;

    private readonly SensorService sensorService;

    public EventFunction(
        ILogger<EventFunction> log,
        SensorService sensorService)
    {
        this.log = log;
        this.sensorService = sensorService;
    }

    [FunctionName("EventFunction")]
    public async Task Run([IoTHubTrigger("", Connection = "HubConnectionString")] EventData message)
    {
        var json = Encoding.UTF8.GetString(message.Body.Span);
        log.InfoEventReceived(json);

        var entity = JsonSerializer.Deserialize<SensorEntity>(json);
        if (entity is not null)
        {
            entity.Timestamp = message.EnqueuedTime;
            await sensorService.UpdateSensorAsync(entity).ConfigureAwait(false);
        }
    }
}
