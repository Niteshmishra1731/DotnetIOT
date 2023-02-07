using DotnetIot.DTO_s;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System.Text;

namespace DotnetIot.Repository
{
    public class SendTelemetryMessages
    {
        private static string connectionstring = "HostName=IotProject301.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=IFQC+PQWtwR3nUwbXhE+ggP0ps9KXslwVOt3udWP9CI=";
        public static RegistryManager registryManager;
        public static DeviceClient client = null;
        public static string myDeviceConnection = "HostName=IotProject301.azure-devices.net;DeviceId=Nitesh;SharedAccessKey=NIhCMnxKgc+vPhaoa2muNBxGdJLMNMz0v6lvf1HUQXc=";
        public static async Task SendMessage(string deviceName)
        {
            try
            {
                registryManager = RegistryManager.CreateFromConnectionString(connectionstring);
                var device = await registryManager.GetTwinAsync(deviceName);
                ReportedProperties properties = new ReportedProperties();
                TwinCollection reportedprop;
                reportedprop =device.Properties.Reported;
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                while (true)
                {
                    var telemetry = new
                    {
                        temperature = reportedprop["temperature"],
                        pressure = reportedprop["pressure"],

                    };
                    var telemetryString = JsonConvert.SerializeObject(telemetry);
                    var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(telemetryString));
                    await client.SendEventAsync(message);
                    Console.WriteLine("{0}>Sending message{1}", DateTime.Now, telemetryString);
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           }
        }
    }

