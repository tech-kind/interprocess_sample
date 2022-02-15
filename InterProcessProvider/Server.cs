using MessagePipe;

namespace InterProcessProvider
{
    public class Server<TReq, TRes>
    {
        private readonly string _topicNameReq = "";
        private readonly string _topicNameRes = "";
        private readonly IDistributedSubscriber<string, TReq> _subscription;
        private readonly IDistributedPublisher<string, TRes> _publisher;
        private Func<TReq, TRes>? _handler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicName"></param>
        /// <param name="subscriber"></param>
        /// <param name="publisher"></param>
        public Server(string topicName, IDistributedSubscriber<string, TReq> subscriber,
            IDistributedPublisher<string, TRes> publisher)
        {
            _topicNameReq = $"{topicName}/req";
            _topicNameRes = $"{topicName}/res";
            _subscription = subscriber;
            _publisher = publisher;

            _subscription.SubscribeAsync(_topicNameReq, SubscriptionCallback);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public void Recieve(Func<TReq, TRes> handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        private void SubscriptionCallback(TReq req)
        {
            if (_handler == null) return;

            var res = _handler(req);
            if (res == null) return;

            _publisher.PublishAsync(_topicNameRes, res);
        }
        
    }
}
