namespace Device_Management.Models.DeviceUpdate
{
    public class RaspberryPiUpdate : DeviceUpdate
    {
        public float? Temperature { get; set; }

        public float? Humidity { get; set; }
    }
}
