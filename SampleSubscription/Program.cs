// See https://aka.ms/new-console-template for more information
using InterProcessProvider;
using SampleSubscription;

InterprocessProvider.Init();

var subscriptionNode = new SampleSubscriptionNode();

InterprocessProvider.Spin();
