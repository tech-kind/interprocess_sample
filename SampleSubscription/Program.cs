// See https://aka.ms/new-console-template for more information
using InterProcessProvider;
using SampleSubscription;

var provider = InterprocessProvider.init("127.0.0.1", 3125);

var subscriptionNode = new SampleSubscriptionNode(provider);

InterprocessProvider.Spin();
