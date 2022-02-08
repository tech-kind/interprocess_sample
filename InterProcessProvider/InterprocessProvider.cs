using MessagePipe;
using Microsoft.Extensions.DependencyInjection;

namespace InterProcessProvider
{
    public static class InterprocessProvider
    {
        public static bool _initialized = false;

        public static ServiceProvider init(string ipAdress, int port)
        {
            ServiceCollection services = new ServiceCollection();

            services.AddMessagePipe();
            services.AddMessagePipeUdpInterprocess(ipAdress, port);

            _initialized = true;

            return services.BuildServiceProvider();
        }

        public static void Shutdown()
        {
            if (!_initialized)
            {
                return;
            }
            _initialized = false;
        }

        public static bool Ok()
        {
            return _initialized;
        }

        public static void Spin(int timeoutMilliSec = 10)
        {
            var spinner = new SpinWait();

            while (_initialized)
            {
                spinner.SpinOnce(timeoutMilliSec);
            }
        }
    }
}