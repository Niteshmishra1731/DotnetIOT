using Microsoft.AspNetCore.Mvc;
using Device = Microsoft.Azure.Devices.Device;
//using IotDevice = DotnetIot.Models.IotDevice;
using DotnetIot.Repository;

namespace DotnetIot.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class IotDeviceController : ControllerBase
    {
        static Device device;
        [HttpPost]
        [Route("AddDevice")]
        public async Task AddDevice(string deviceId)
        {
            await IotDevice.AddDeviceAsync(deviceId);
        }
        [HttpGet]
        [Route("GetDevice")]
        public async Task<Device> GetDevice(string deviceId)
        {
            device=await IotDevice.GetDeviceAsync(deviceId);
            return device;

        }
        [HttpDelete]
        [Route("RemoveDevice")]
        public async Task RemoveDevice(string deviceId)
        {
            await IotDevice.RemoveDeviceAsync(deviceId);

        }
        [HttpPut]
        [Route("UpdateDevice")]
        public async Task UpdateDevice(string deviceId)
        {
            await IotDevice.UpdateDeviceAsync(deviceId);
        }



        }
    }

