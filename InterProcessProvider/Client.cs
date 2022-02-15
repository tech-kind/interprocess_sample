using MessagePipe;

namespace InterProcessProvider
{
    public class Client<TReq, TRes>
    {
        private readonly string _topicNameReq = "";
        private readonly string _topicNameRes = "";
        private readonly IDistributedPublisher<string, TReq> _publisher;
        private readonly IDistributedSubscriber<string, TRes> _subscription;
        private TRes? _result;
        private bool _isComplete;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicName"></param>
        /// <param name="publisher"></param>
        /// <param name="subscriber"></param>
        public Client(string topicName, IDistributedPublisher<string, TReq> publisher,
            IDistributedSubscriber<string, TRes> subscriber)
        {
            _isComplete = false;

            _topicNameReq = $"{topicName}/req";
            _topicNameRes = $"{topicName}/res";
            _publisher = publisher;
            _subscription = subscriber;

            _subscription.SubscribeAsync(_topicNameRes, SubscriptionCallback);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<TRes?> Call(TReq req)
        {
            _isComplete = false;
            _ = _publisher.PublishAsync(_topicNameReq, req);
            await WaitingSubscription();
            return _result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="res"></param>
        private void SubscriptionCallback(TRes res)
        {
            _result = res;
            _isComplete = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task WaitingSubscription()
        {
            await Task.Run(() =>
            {
                SpinWait wait = new SpinWait();
                while (!_isComplete)
                {
                    wait.SpinOnce(1);
                }
            });
        }
    }
}
