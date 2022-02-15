using MessagePipe;
using Microsoft.Extensions.DependencyInjection;

namespace InterProcessProvider
{
    public abstract class Node : IDisposable
    {
        /// <summary>
        /// Create a new instance.
        /// </summary>
        public Node()
        {
            _serviceProviders = new List<ServiceProvider>();
        }

        private List<ServiceProvider> _serviceProviders;

        #region dispose
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            foreach (var serviceProvider in _serviceProviders)
            {
                serviceProvider.Dispose();
            }
        }

        #endregion

        #region pub/sub
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="topicName"></param>
        /// <returns></returns>
        protected Publisher<TMessage> CreatePublisher<TMessage>(string topicName)
            where TMessage : notnull
        {
            var provider = InterprocessProvider.CreateProvider(topicName);
            _serviceProviders.Add(provider);
            var pub = provider.GetRequiredService<IDistributedPublisher<string, TMessage>>();
            return new Publisher<TMessage>(topicName, pub);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="topicName"></param>
        /// <returns></returns>
        protected Subscription<TMessage> CreateSubscription<TMessage>(string topicName)
            where TMessage : notnull
        {
            var provider = InterprocessProvider.CreateProvider(topicName);
            _serviceProviders.Add(provider);
            var sub = provider.GetRequiredService<IDistributedSubscriber<string, TMessage>>();
            return new Subscription<TMessage>(topicName, sub);
        }
        #endregion

        #region server/client
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TReq"></typeparam>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="topicName"></param>
        /// <returns></returns>
        protected Server<TReq, TRes> CreateServer<TReq, TRes>(string topicName)
        {
            var topicNameReq = $"{topicName}/req";
            var topicNameRes = $"{topicName}/res";
            var providerReq = InterprocessProvider.CreateProvider(topicNameReq);
            var providerRes = InterprocessProvider.CreateProvider(topicNameRes);
            _serviceProviders.Add(providerReq);
            _serviceProviders.Add(providerRes);
            var sub = providerReq.GetRequiredService<IDistributedSubscriber<string, TReq>>();
            var pub = providerRes.GetRequiredService<IDistributedPublisher<string, TRes>>();
            return new Server<TReq, TRes>(topicName, sub, pub);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TReq"></typeparam>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="topicName"></param>
        /// <returns></returns>
        protected Client<TReq, TRes> CreateClient<TReq, TRes>(string topicName)
        {
            var topicNameReq = $"{topicName}/req";
            var topicNameRes = $"{topicName}/res";
            var providerReq = InterprocessProvider.CreateProvider(topicNameReq);
            var providerRes = InterprocessProvider.CreateProvider(topicNameRes);
            _serviceProviders.Add(providerReq);
            _serviceProviders.Add(providerRes);
            var pub = providerReq.GetRequiredService<IDistributedPublisher<string, TReq>>();
            var sub = providerRes.GetRequiredService<IDistributedSubscriber<string, TRes>>();
            return new Client<TReq, TRes>(topicName, pub, sub);
        }
        #endregion
    }
}
