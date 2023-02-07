using DotnetIot.Repository;
using Microsoft.AspNetCore.Mvc;
namespace MyCoreApi.IotDeviceController;




    [ApiController]
    [Route("[controller]")]
    public class TelemetryController : ControllerBase
    {
        [HttpPost("SendTelemetryMessage")]


        public async Task<string> SendMessage(string deviceName)
        {
            await SendTelemetryMessages.SendMessage(deviceName);
            return null;
        }
   }



        
