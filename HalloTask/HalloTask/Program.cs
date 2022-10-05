Console.WriteLine("Hello, World!");

var t1 = new Task(() =>
{
    Console.WriteLine("T1 gestartet");
    Thread.Sleep(3000);
    throw new OutOfMemoryException();
    Console.WriteLine("T1 fertig");
});

var tc1 = t1.ContinueWith(t =>
{
    Console.WriteLine("T1 continue");
});

var t1_OK = t1.ContinueWith(t =>
{
    Console.WriteLine("T1 OK");
}, TaskContinuationOptions.OnlyOnRanToCompletion);


var t1_Err = t1.ContinueWith(t =>
{
    Console.WriteLine($"T1 ERROR {t.Exception.InnerException}");
}, TaskContinuationOptions.OnlyOnFaulted);

var t2 = new Task<long>(() =>
{
    Console.WriteLine("T2 gestartet");
    Thread.Sleep(2000);
    Console.WriteLine("T2 fertig");
    return 8974796453789435978;
});

t1.Start();
t2.Start();
t2.Wait();
Console.WriteLine($"T2 result: {t2.Result}");

Console.WriteLine("Ende");
Console.ReadLine();