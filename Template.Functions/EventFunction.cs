namespace Template.Functions;

using System.Text;
using System.Text.Json;

using Azure.Messaging.EventHubs;

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

using Template.Models;
using Template.Services;

using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

public class EventFunction
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
        log.LogInformation("Event received: message=[{Json}]", json);

        var entity = JsonSerializer.Deserialize<SensorEntity>(json);
        if (entity is not null)
        {
            entity.Timestamp = message.EnqueuedTime.DateTime;
            await sensorService.UpdateSensorAsync(entity).ConfigureAwait(false);
        }
    }
}
