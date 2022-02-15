// See https://aka.ms/new-console-template for more information
using InterProcessProvider;
using SampleClient;

InterprocessProvider.Init();

var clientNode = new SampleClientNode();

InterprocessProvider.Spin();
