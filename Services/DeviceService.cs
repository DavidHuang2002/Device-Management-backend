using Device_Management.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Device_Management.Services
{
    public class DeviceService
    {
        private readonly DeviceManagementDbContext _context;

        public DeviceService(DeviceManagementDbContext context)
        {
            _context = context;
        }



        public async Task<Device> UpdateDeviceStateAsync(int deviceId, JObject newState)
        {
            // Step 1: Check if a device with the given deviceId exists in the device table
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == deviceId);
            if (device == null)
            {
                throw new ArgumentException($"Device with ID {deviceId} does not exist.");
            }

            var deviceType = device.Type;

            switch (deviceType)
            {
                case "Raspberry Pi Web Client":
                    await UpdateDeviceStateRaspberryPi(deviceId, newState);
                    break;

                // TODO default for no incomplete device
            }

            // Step 4: Update the lastCheckIn time of the device in the device table
            device.LastCheckInTime = DateTime.Now;

            // Step 5: Save changes to the database
            await _context.SaveChangesAsync();
            Console.WriteLine("Successful update device's state");
            return device;
        }

        // TODO check how polymorphism work in Cs -- how to return the data of RaspberryPi.
        public async Task<Device> UpdateDeviceStateRaspberryPi(int deviceId, JObject newState)
        {
            // TODO the changes created in this scope might not be saved?
            // Step 2: Check if an entry with the given deviceId exists in the RaspberryPi table
            var raspberryPi = await _context.RaspberryPi.FirstOrDefaultAsync(rp => rp.Id == deviceId);

            // Step 3: Update or create a new entry in the RaspberryPi table based on newState
            if (raspberryPi == null)
            {
                // TODO: has bugs
                raspberryPi = new RaspberryPi
                {
                    Id = deviceId,
                    Temperature = newState.ContainsKey("temperature") ? newState["temperature"].Value<float>() : (float?)null,
                    Humidity = newState.ContainsKey("humidity") ? newState["humidity"].Value<float>() : (float?)null
                };
                _context.RaspberryPi.Add(raspberryPi);
            }
            else
            {
                raspberryPi.Temperature = newState.ContainsKey("temperature") ? newState["temperature"].Value<float>() : raspberryPi.Temperature;
                raspberryPi.Humidity = newState.ContainsKey("humidity") ? newState["humidity"].Value<float>() : raspberryPi.Humidity;
            }

            return raspberryPi;
        }
    }
}
