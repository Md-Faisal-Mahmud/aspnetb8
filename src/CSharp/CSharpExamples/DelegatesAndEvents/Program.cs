// See https://aka.ms/new-console-template for more information
using DelegatesAndEvents;

List<Person> persons = new List<Person>();
persons.Add(new Person { Name = "Tareq", Age = 23 });
persons.Add(new Person { Name = "Hasan", Age = 23 });
persons.Add(new Person { Name = "Farid", Age = 32 });
persons.Add(new Person { Name = "Tareq", Age = 42 });
persons.Add(new Person { Name = "Asad", Age = 30 });

BubbleSort<Person> bubbleSort= new BubbleSort<Person>();
var person = bubbleSort.Sort(persons, CompareDouble);

int CompareDouble(Person x, Person y)
{
    if (x.Name == y.Name)
    {
        if (x.Age == y.Age)
            return 0;
        else if (x.Age > y.Age)
            return 1;
        else
            return -1;
    }
    else
        return x.Name.CompareTo(y.Name);

}

foreach (var n in persons)
{
    Console.WriteLine($"Name: {n.Name}, Age: {n.Age}");
}

Console.WriteLine();

Engine engine = new Engine();
engine.Overheat += WhenOverheat;
engine.Overheat += ShutDown;
engine.Start();
engine.SpeedUp(60);

void WhenOverheat(int heat)
{
    Console.WriteLine($"Alert > Engine heat reached: {heat}");
}

void ShutDown(int heat)
{
    if (heat > 1000)
    {
        engine.Stop();
        Console.WriteLine("Stopping engine");
    }

}