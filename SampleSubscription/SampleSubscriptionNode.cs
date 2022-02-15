using InterProcessProvider;
using Microsoft.Extensions.DependencyInjection;

namespace SampleSubscription
{
    public class SampleSubscriptionNode : Node
    {
        private readonly Subscription<int> _subscription;

        /// <summary>
        /// 
        /// </summary>
        public SampleSubscriptionNode()
            : base()
        {
            _subscription = CreateSubscription<int>("/api/test");
            
            _subscription.Subscribe(CountCallback);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        private void CountCallback(int count)
        {
            Console.WriteLine($"[subscribe] count={count}");
        }
    }
}
