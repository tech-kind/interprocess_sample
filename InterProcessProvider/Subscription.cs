using MessagePipe;

namespace InterProcessProvider
{
    public class Subscription<TMessage>
    {
        private readonly string _topicName = "";
        private readonly IDistributedSubscriber<string, TMessage> _subscription;

        public Subscription(string topicName, IDistributedSubscriber<string, TMessage> subscription)
        {
            _topicName = topicName;
            _subscription = subscription;
        }

        public ValueTask<IAsyncDisposable> Subscribe(IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return _subscription.SubscribeAsync(_topicName, handler, filters);
        }

        public ValueTask<IAsyncDisposable> Subscribe(Action<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return _subscription.SubscribeAsync(_topicName, handler, filters);
        }

        public ValueTask<IAsyncDisposable> Subscribe(Action<TMessage> handler, Func<TMessage, bool> predicate, params MessageHandlerFilter<TMessage>[] filters)
        {
            return _subscription.SubscribeAsync(_topicName, handler, predicate, filters);
        }

    }
}
