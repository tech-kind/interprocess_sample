using InterProcessProvider;
using Microsoft.Extensions.DependencyInjection;

namespace SamplePublisher
{
    public class SamplePublisherNode : Node
    {
        private readonly Publisher<int> _publisher;
        private int _count = 0;

        public SamplePublisherNode(ServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _publisher = CreatePublisher<int>("/api/test");

            var timer = new System.Timers.Timer(100);

            // タイマーの処理
            timer.Elapsed += (sender, e) =>
            {
                _count++;
                _publisher.Publish(_count);
            };

            // タイマーを開始する
            timer.Start();
        }
    }
}
