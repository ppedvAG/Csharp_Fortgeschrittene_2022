// See https://aka.ms/new-console-template for more information
using Castle.DynamicProxy.Contributors;
using ppedv.Fuhrparkverwaltung.Logic.FuhrparkService;
using ppedv.Fuhrparkverwaltung.Model;
using ppedv.Fuhrparkverwaltung.Model.Contracts;

Console.WriteLine("Hello, World!");

var conString = "Server=(localdb)\\mssqllocaldb;Database=Fuhrpark_dev;Trusted_Connection=true";
IRepository repo = new ppedv.Fuhrparkverwaltung.Data.EfCore.EfRepository(conString);
FuhrparkManager fpm = new FuhrparkManager(repo);

foreach (var auto in repo.GetAll<Auto>())
{
    Console.WriteLine($"{auto.Hersteller} {auto.Model} {auto.Farbe} {auto.Leistung} {auto.Baujahr:d} [{auto.Garage?.Ort}]");
}

var cg = fpm.GetGarageWithFastestCars();
Console.WriteLine($"Coolste Garage: {cg?.Ort}");

Console.WriteLine("Ende");