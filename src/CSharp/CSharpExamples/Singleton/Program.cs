// See https://aka.ms/new-console-template for more information
using Singleton;

Console.WriteLine("Hello, World!");

ILogger logger = Logger.GetLogger();

logger.Log("Hello");
