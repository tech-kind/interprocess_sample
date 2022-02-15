// See https://aka.ms/new-console-template for more information
using InterProcessProvider;
using SampleServer;

InterprocessProvider.Init();

var serverNode = new SampleServerNode();

InterprocessProvider.Spin();
