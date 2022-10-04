// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");



foreach (var item in GetStrings())
{
    Console.WriteLine(item);
}


IEnumerable<string> GetStrings()
{
    yield return "jkhwebf";
    yield return "jkhwebf";
    yield return "jkhwebf";
    yield return "jkhwebf";
}

IEnumerable<string> GetStringsDoof()
{
    var result = new List<string>();

    result.Add("jkhwebf");
    result.Add("jkhwebf");
    result.Add("jkhwebf");
    result.Add("jkhwebf");
    return result;
}