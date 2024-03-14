// See https://aka.ms/new-console-template for more information
using FactoryPattern;

Console.WriteLine("Hello, World!");

Car car = CarFactory.Create("toyota");
Console.WriteLine(car.model);


Printer p = Printer.Create("m-234", 2000);