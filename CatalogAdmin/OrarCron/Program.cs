// See https://aka.ms/new-console-template for more information

using OrarCron;

var OrarCron = new OrarProvider();
await OrarCron.StartCron();

// Console.WriteLine(OrarCron);