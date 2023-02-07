using Microsoft.Azure.Devices;

namespace DotnetIot.Repository
{
    public class IotDevice
    {
        public static RegistryManager registryManager;
        private static string connectionstring = "HostName=IotProject301.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=IFQC+PQWtwR3nUwbXhE+ggP0ps9KXslwVOt3udWP9CI=";
        //static Device device;
        public static async Task AddDeviceAsync(string deviceName)
        {
            Device device;
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Device name please");
            }
            registryManager = RegistryManager.CreateFromConnectionString(connectionstring);
            device = await registryManager.AddDeviceAsync(new Device(deviceName));
            return;
        }
        public static async Task<Device> GetDeviceAsync(string deviceId)
        {
            Device device;
            registryManager = RegistryManager.CreateFromConnectionString(connectionstring);
            device = await registryManager.GetDeviceAsync(deviceId);
            return device;
        }
        public static async Task RemoveDeviceAsync(string deviceId)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionstring);
            await registryManager.RemoveDeviceAsync(deviceId);
        }
        public static async Task<Device> UpdateDeviceAsync(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("device id please");
            }
            Device device = new Device(deviceId);
            registryManager = RegistryManager.CreateFromConnectionString(connectionstring);
            device = await registryManager.GetDeviceAsync(deviceId);
            device.StatusReason = "Updated";
            device = await registryManager.UpdateDeviceAsync(device);
            return device;
        }

    }
}
