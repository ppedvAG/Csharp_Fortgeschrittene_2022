// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Parallel.For(0, 100000000, new ParallelOptions() { MaxDegreeOfParallelism = 4 }, i => Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}"));

List<string> text = new List<string>();

var result = text.Where(x => x.StartsWith("b")).AsParallel();

//Parallel.Invoke(Zähle, Zähle, Zähle, Zähle);


void Zähle()
{
    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
    }
}
