using DotnetIot.DTO_s;
using DotnetIot.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices.Shared;
using System.Reflection.Metadata.Ecma335;

namespace DotnetIot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IotDevicePropertiesController : Controller
    {
        [HttpPut("UpdateDeviceReportedProperties")]
        public async Task<string> UpdateDeviceReportedProperties(string deviceName, ReportedProperties properties)
        {
            await IotDeviceProperties.AddReportedPropertiesAsync(deviceName, properties);
            return null;
        }
        [HttpPut("UpdateDeviceDesiredProperties")]
        public async Task<string> UpdateDeviceDesiredProperties(string deviceName)
        {
            await IotDeviceProperties.DesirePropertiesUpdate(deviceName);
            return null;
        }
        [HttpPut("UpdateIotdeviceTagProperties")]
        public async Task<string> UpdateDesiredTagProperties(string DeviceName)
        {
            await IotDeviceProperties.UpdateDeviceTagProperties(DeviceName);
            return null;
        }
        [HttpGet("GetIotDeviceProperties")]
        public async Task<Twin> GetIotDevice(string deviceName)
        {
            var device = await IotDeviceProperties.GetDevicePropertiesAsyc(deviceName);
            return null;
        }


    }
}

