using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using DotnetIot.DTO_s;

namespace DotnetIot.Repository
{
    public class IotDeviceProperties
    {
        private static string connectionstring = "HostName=IotProject301.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=IFQC+PQWtwR3nUwbXhE+ggP0ps9KXslwVOt3udWP9CI=";
        public static RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionstring);
        public static DeviceClient client = null;
        public static string myDeviceConnection = "HostName=IotProject301.azure-devices.net;DeviceId=Nitesh;SharedAccessKey=NIhCMnxKgc+vPhaoa2muNBxGdJLMNMz0v6lvf1HUQXc=";
        public static async Task AddReportedPropertiesAsync(string deviceName, ReportedProperties properties)
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Valid device name please");
            }
            else
            {
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection reportedProperties, connectivity;
                reportedProperties = new TwinCollection();
                connectivity = new TwinCollection();
                connectivity["type"] = "cellular";
                reportedProperties["connectivity"] = "connectivity";
                reportedProperties["temperature"] = properties.temperature;
                reportedProperties["pressure"] = properties.pressure;
                await client.UpdateReportedPropertiesAsync(reportedProperties);
                return;

            }
        }
        public static async Task DesirePropertiesUpdate(string deviceName)
        {
            client = DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            var device = await registryManager.GetTwinAsync(deviceName);
            TwinCollection desiredproperties, telemetryconfig;
            desiredproperties = new TwinCollection();
            telemetryconfig = new TwinCollection();
            telemetryconfig["frequency"] = "5Hz";
            desiredproperties["telemetryconfig(type)"] = telemetryconfig;
            device.Properties.Desired["telemtryconfig"] = telemetryconfig;
            await registryManager.UpdateTwinAsync(device.DeviceId, device, device.ETag);
            return;

        }
        public static async Task<Twin> GetDevicePropertiesAsyc(string deviceName)
        {
            var device = await registryManager.GetTwinAsync(deviceName);
            return device;
        }
        public static async Task UpdateDeviceTagProperties(string deviceName)
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Valid device name please");
            }
            else
            {
                var twin = await registryManager.GetTwinAsync(deviceName);
                var patchData =
                @"{
                tags:{
                    location:{
                        region:'Canada',
                        plant:'IOTPro'
                    }
                }

            }";
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection connectivity;
                connectivity = new TwinCollection();
                connectivity["type"] = "cellular";
                await registryManager.UpdateTwinAsync(twin.DeviceId, patchData, twin.ETag);
                return;
            }
        }

    }
}
