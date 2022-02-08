using InterProcessProvider;
using Microsoft.Extensions.DependencyInjection;

namespace SampleSubscription
{
    public class SampleSubscriptionNode : Node
    {
        private readonly Subscription<int> _subscription;

        public SampleSubscriptionNode(ServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _subscription = CreateSubscription<int>("/api/test");
            _subscription.Subscribe(x => Console.WriteLine(x));
        }
    }
}
