using InterProcessProvider;

namespace SampleClient
{
    public class SampleClientNode : Node
    {
        private readonly Client<int, int> _client;
        private int _count = 0;

        /// <summary>
        /// 
        /// </summary>
        public SampleClientNode()
            : base()
        {
            _client = CreateClient<int, int>("/api/test2");

            var timer = new System.Timers.Timer(10000);
            timer.Elapsed += OnTimer;

            // タイマーを開始する
            timer.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnTimer(object? sender, EventArgs e)
        {
            _count++;
            Console.WriteLine($"[publish] count={_count}");
            var res = await _client.Call(_count);
            Console.WriteLine($"[publish] callback={res}");
        }
    }
}
