namespace DeviceEmulator.Services
{
    public interface IEmulatorService
    {
        double GenerateRandomTemperature(double minTemp, double maxTemp);
    }
}