using MessagePipe;
using Microsoft.Extensions.DependencyInjection;

namespace InterProcessProvider
{
    public abstract class Node : IDisposable
    {
        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public Node(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private readonly ServiceProvider _serviceProvider;

        #region dispose

        public void Dispose()
        {

        }

        #endregion

        #region message
        /// <summary>
        /// Creates a message publisher for the specified type.
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <returns></returns>
        protected Publisher<TMessage> CreatePublisher<TMessage>(string topicName)
            where TMessage : notnull
        {
            var pub = _serviceProvider.GetRequiredService<IDistributedPublisher<string, TMessage>>();
            return new Publisher<TMessage>(topicName, pub);
        }

        /// <summary>
        /// Creates a message subscriber for the specified type.
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <returns></returns>
        protected Subscription<TMessage> CreateSubscription<TMessage>(string topicName)
            where TMessage : notnull
        {
            var sub = _serviceProvider.GetRequiredService<IDistributedSubscriber<string, TMessage>>();
            return new Subscription<TMessage>(topicName, sub);
        }
        #endregion
    }
}
