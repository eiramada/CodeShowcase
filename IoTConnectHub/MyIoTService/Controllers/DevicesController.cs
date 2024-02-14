using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyIoTService.Data.Data;
using MyIoTService.Data.Entities;
using MyIoTService.DTO;

namespace MyIoTService.Controllers
{
    /// <summary>
    /// Controller for managing IoT devices.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly MyIoTDataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="context">The database context used for accessing IoT devices.</param>
        public DevicesController(MyIoTDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Authenticates a device by its name.
        /// </summary>
        /// <param name="deviceName">The name of the device to authenticate.</param>
        /// <response code="200">Returns the device ID if authentication is successful.</response>
        /// <response code="404">Returned if the device is not found.</response>
        [HttpGet("authenticate")]
        public async Task<IActionResult> AuthenticateDevice(string deviceName)
        {
            var device = await _context.Devices
                                       .FirstOrDefaultAsync(d => d.DeviceName == deviceName);

            if (device == null)
            {
                return NotFound(new { Message = "Device not found." });
            }

            return Ok(new { device.DeviceId });
        }

        /// <summary>
        /// Retrieves a device by its ID.
        /// </summary>
        /// <param name="deviceId">The unique identifier of the device.</param>
        /// <response code="200">Returns the requested device.</response>
        /// <response code="404">Returned if no device is found for the provided ID.</response>
        [HttpGet("{deviceId}")]
        public async Task<IActionResult> GetDevice(int deviceId)
        {
            Device? device = await _context.Devices.FindAsync(deviceId);
            if (device == null)
            {
                return NotFound();
            }
            return Ok(device);
        }

        /// <summary>
        /// Retrieves all registered devices.
        /// </summary>
        /// <response code="200">Returns a list of all devices.</response>
        [HttpGet("all")]
        public ActionResult<List<Device>> GetDevices()
        {
            List<Device> devices = _context.Devices.ToList();
            return Ok(devices);
        }

        /// <summary>
        /// Creates a new device.
        /// </summary>
        /// <param name="deviceDto">The data transfer object containing the device information.</param>
        /// <response code="201">Returns the created device.</response>
        /// <response code="400">Returned if the provided data is invalid.</response>
        [HttpPost]
        public async Task<IActionResult> CreateDevice([FromBody] DeviceDto deviceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Device device = new Device
            {
                DeviceName = deviceDto.DeviceName,
                UserId = deviceDto.UserId,
            };

            if (deviceDto.DeviceId != 0)
            {
                device.DeviceId = deviceDto.DeviceId;
            }

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDevices), new { deviceId = device.DeviceId }, device);
        }
    }
}
