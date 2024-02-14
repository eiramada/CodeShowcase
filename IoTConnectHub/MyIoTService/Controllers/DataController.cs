using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyIoTService.Data.Data;
using MyIoTService.Data.Entities;
using MyIoTService.DTO;

namespace MyIoTService.Data.Controllers
{
    /// <summary>
    /// Controller for managing IoT device data.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly MyIoTDataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataController"/> class.
        /// </summary>
        /// <param name="context">The database context used for accessing IoT device data.</param>
        public DataController(MyIoTDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves device data by its identifier.
        /// </summary>
        /// <param name="dataId">The unique identifier of the device data.</param>
        /// <response code="200">Returns the requested device data.</response>
        /// <response code="404">Returned if no data is found for the provided ID.</response>
        [HttpGet("{dataId}")]
        public async Task<IActionResult> GetDataById(int dataId)
        {
            DeviceData? deviceData = await _context.DeviceData.FindAsync(dataId);
            if (deviceData == null)
            {
                return NotFound();
            }
            return Ok(deviceData);
        }

        /// <summary>
        /// Retrieves all data for a specific device.
        /// </summary>
        /// <param name="deviceId">The unique identifier of the device.</param>
        /// <response code="200">Returns all data associated with the specified device.</response>
        /// <response code="404">Returned if no data is found for the specified device.</response>
        [HttpGet("/device/{deviceId}")]
        public async Task<IActionResult> GetAllDataForDevice(int deviceId)
        {
            List<DeviceData> deviceData = await _context.DeviceData
                    .Where(dd => dd.DeviceId == deviceId)
                    .ToListAsync();

            if (!deviceData.Any())
            {
                return NotFound();
            }

            return Ok(deviceData);
        }

        /// <summary>
        /// Creates and stores new data for a specific device.
        /// </summary>
        /// <param name="deviceId">The unique identifier of the device for which the data is being created.</param>
        /// <param name="deviceDataDto">The data transfer object containing the data to be stored.</param>
        /// <response code="200">Returns the created device data.</response>
        /// <response code="400">Returned if the provided data is invalid.</response>
        [HttpPost("{deviceId}")]
        public async Task<IActionResult> CreateData(int deviceId, [FromBody] DeviceDataDTO deviceDataDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DeviceData data = new DeviceData
            {
                TimeStamp = DateTime.UtcNow,
                Value = deviceDataDto.Value,
                DeviceId = deviceId,
            };

            _context.DeviceData.Add(data);
            await _context.SaveChangesAsync();

            return Ok(data);
        }
    }
}
