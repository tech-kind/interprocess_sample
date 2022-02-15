using MessagePipe;

namespace InterProcessProvider
{
    public class Publisher<TMessage>
    {
        private readonly string _topicName = "";
        private readonly IDistributedPublisher<string, TMessage> _publisher;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicName"></param>
        /// <param name="publisher"></param>
        public Publisher(string topicName, IDistributedPublisher<string, TMessage> publisher)
        {
            _topicName = topicName;
            _publisher = publisher;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Publish(TMessage message)
        {
            _publisher.PublishAsync(_topicName, message);
        }

    }
}
