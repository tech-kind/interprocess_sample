// See https://aka.ms/new-console-template for more information
using InterProcessProvider;
using SamplePublisher;

InterprocessProvider.Init();

var publisherNode = new SamplePublisherNode();

InterprocessProvider.Spin();