namespace DeviceEmulator.Services
{
    public class EmulatorService : IEmulatorService
    {
        private readonly Random _random = new Random();

        public double GenerateRandomTemperature(double minTemp, double maxTemp)
        {
            return Math.Round(_random.NextDouble() * (maxTemp - minTemp) + minTemp, 1);
        }
    }
}
