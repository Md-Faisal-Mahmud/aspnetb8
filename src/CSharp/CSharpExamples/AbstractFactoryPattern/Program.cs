// See https://aka.ms/new-console-template for more information
using AbstractFactoryPattern;

Console.WriteLine("Hello, World!");

FighterFactory factory = new MigFactory();
Fighter fighter = factory.CreateFighter();
fighter.Weapon = factory.CreateWeapon();

Console.WriteLine(fighter);
Console.WriteLine(fighter.Weapon);