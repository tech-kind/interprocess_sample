using MessagePipe;
using Microsoft.Extensions.DependencyInjection;

namespace InterProcessProvider
{

    public static class InterprocessProvider
    {
        public static bool _initialized = false;

        /// <summary>
        /// 
        /// </summary>
        public static void Init()
        {
            _initialized = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="asServer"></param>
        /// <returns></returns>
        public static ServiceProvider CreateProvider(string name, bool asServer = false)
        {
            ServiceCollection services = new ServiceCollection();

            services.AddMessagePipe();
            services.AddMessagePipeNamedPipeInterprocess(name, x =>
            {
                x.HostAsServer = asServer;
            });

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Shutdown()
        {
            if (!_initialized)
            {
                return;
            }
            _initialized = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool Ok()
        {
            return _initialized;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeoutMilliSec"></param>
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