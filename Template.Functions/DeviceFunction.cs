using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Template.Functions
{
    using Azure.Messaging.EventHubs;

    public class DeviceFunction
    {
        [FunctionName("Function1")]
        public void Run([IoTHubTrigger("", Connection = "HubConnectionString")]EventData message, ILogger log)
        {
            //log.LogInformation($"C# IoT Hub trigger function processed a message: {Encoding.UTF8.GetString(message.Body.Array)}");
        }
    }
}
