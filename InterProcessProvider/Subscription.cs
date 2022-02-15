using MessagePipe;

namespace InterProcessProvider
{
    public class Subscription<TMessage>
    {
        private readonly string _topicName = "";
        private readonly IDistributedSubscriber<string, TMessage> _subscription;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicName"></param>
        /// <param name="subscription"></param>
        public Subscription(string topicName, IDistributedSubscriber<string, TMessage> subscription)
        {
            _topicName = topicName;
            _subscription = subscription;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public ValueTask<IAsyncDisposable> Subscribe(IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return _subscription.SubscribeAsync(_topicName, handler, filters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public ValueTask<IAsyncDisposable> Subscribe(Action<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return _subscription.SubscribeAsync(_topicName, handler, filters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="predicate"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public ValueTask<IAsyncDisposable> Subscribe(Action<TMessage> handler, Func<TMessage, bool> predicate, params MessageHandlerFilter<TMessage>[] filters)
        {
            return _subscription.SubscribeAsync(_topicName, handler, predicate, filters);
        }

    }
}
