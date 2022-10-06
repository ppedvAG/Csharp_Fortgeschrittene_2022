// See https://aka.ms/new-console-template for more information
using Autofac;
using ppedv.Fuhrparkverwaltung.Data.EfCore;
using ppedv.Fuhrparkverwaltung.Logic.FuhrparkService;
using ppedv.Fuhrparkverwaltung.Model;
using ppedv.Fuhrparkverwaltung.Model.Contracts;
using System.Reflection;

Console.WriteLine("Hello, World!");

var conString = "Server=(localdb)\\mssqllocaldb;Database=Fuhrpark_dev;Trusted_Connection=true";

//DI per Refernce
//IRepository repo = new ppedv.Fuhrparkverwaltung.Data.EfCore.EfRepository(conString);

//DI per Reflection
//var filePath = @"C:\Users\Fred\source\repos\ppedvAG\Csharp_Fortgeschrittene_2022\ppedv.Fuhrparkverwaltung\ppedv.Fuhrparkverwaltung.Data.EfCore\bin\Debug\net6.0\ppedv.Fuhrparkverwaltung.Data.EfCore.dll";
//var ass = Assembly.LoadFrom(filePath);
//Type typeMitIRepo = ass.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IRepository)));
//var repo = (IRepository)Activator.CreateInstance(typeMitIRepo,conString);

//DI per AutoFac
var builder = new ContainerBuilder();
//builder.RegisterType<EfRepository>().AsImplementedInterfaces();
builder.Register(_ => new EfRepository(conString)).As<IRepository>();
var container = builder.Build();

FuhrparkManager fpm = new(container.Resolve<IRepository>());
var repo = container.Resolve<IRepository>();
foreach (var auto in repo.GetAll<Auto>())
{
    Console.WriteLine($"{auto.Hersteller} {auto.Model} {auto.Farbe} {auto.Leistung} {auto.Baujahr:d} [{auto.Garage?.Ort}]");
}

var cg = fpm.GetGarageWithFastestCars();
Console.WriteLine($"Coolste Garage: {cg?.Ort}");

Console.WriteLine("Ende");