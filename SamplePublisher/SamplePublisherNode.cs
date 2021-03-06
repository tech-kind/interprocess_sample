using InterProcessProvider;
using Microsoft.Extensions.DependencyInjection;

namespace SamplePublisher
{
    public class SamplePublisherNode : Node
    {
        private readonly Publisher<int> _publisher;
        private int _count = 0;

        /// <summary>
        /// 
        /// </summary>
        public SamplePublisherNode()
            : base()
        {
            _publisher = CreatePublisher<int>("/api/test");

            var timer = new System.Timers.Timer(100);
            timer.Elapsed += OnTimer;

            // タイマーを開始する
            timer.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimer(object? sender, EventArgs e)
        {
            _count++;
            Console.WriteLine($"[publish] count={_count}");
            _publisher.Publish(_count);
        }
    }
}
