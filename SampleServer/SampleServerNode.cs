using InterProcessProvider;

namespace SampleServer
{
    public class SampleServerNode : Node
    {
        private readonly Server<int, int> _server;

        /// <summary>
        /// 
        /// </summary>
        public SampleServerNode()
            : base()
        {
            _server = CreateServer<int, int>("/api/test2");
            _server.Recieve(CountCallback);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int CountCallback(int value)
        {
            Console.WriteLine($"[receive] count={value}");
            return value + 100;
        }
    }
}
