using MessagePipe;

namespace InterProcessProvider
{
    public class Publisher<TMessage>
    {
        private readonly string _topicName = "";
        private readonly IDistributedPublisher<string, TMessage> _publisher;

        public Publisher(string topicName, IDistributedPublisher<string, TMessage> publisher)
        {
            _topicName = topicName;
            _publisher = publisher;
        }

        public void Publish(TMessage message)
        {
            _publisher.PublishAsync(_topicName, message);
        }

    }
}
