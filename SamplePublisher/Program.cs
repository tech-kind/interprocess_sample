// See https://aka.ms/new-console-template for more information
using InterProcessProvider;
using SamplePublisher;

var provider = InterprocessProvider.init("127.0.0.1", 3125);

var publisherNode = new SamplePublisherNode(provider);

InterprocessProvider.Spin();