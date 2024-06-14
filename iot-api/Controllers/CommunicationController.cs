using Microsoft.AspNetCore.Mvc;
using System.IO.Ports;
using iot.db;

namespace iot_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommunicationController : Controller
    {
        private static SerialPort _serialPort;

        iotDbContext _db = null;
        string Email = "jeyaprathap.unilogic@gmail.com";

        IConfiguration configuration;

        public CommunicationController(IConfiguration configuration)
        {
            this.configuration = configuration;
            string connStr = this.configuration.GetConnectionString("iotdb") ?? "";
            _db = new iotDbContext(connStr);

            if (_serialPort == null)
            {
                _serialPort = new SerialPort("COM3", 9600);
                _serialPort.Open();
            }
        }

        [HttpGet]
        public IActionResult Send(string data)
        {
            if (!_serialPort.IsOpen)
            {
                return BadRequest("Serial port is not open");
            }

            try
            {
                _serialPort.WriteLine(data + "\n");

                CommunicationLog log = new CommunicationLog();

                log.Id = Guid.NewGuid().ToString();
                log.OutputDeviceName = "Built-In LED";
                log.Request = data.ToUpper().Contains("ON") ? "ON" : data.ToUpper().Contains("OFF") ? "OFF" : data;
                log.UpdatedDateTime = DateTime.Now;
                log.UpdatedBy = Email ?? "";

                _db.CommunicationLogs.Add(log);
                _db.SaveChanges();


                return Ok(new { status = "Data sent successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest("Error sending data: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Receive()
        {
            if (!_serialPort.IsOpen)
            {
                return BadRequest("Serial port is not open");
            }

            try
            {
                string receivedData = _serialPort.ReadLine();
                Console.WriteLine(receivedData);
                return Ok(new { status = receivedData });
            }
            catch (Exception ex)
            {
                return BadRequest("Error receiving data: " + ex.Message);
            }
        }
    }
}
