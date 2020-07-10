using EFCore.Data;
using EFCore.Domain;
using System;
using System.Linq;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Context.Database.EnsureCreated();
            GetSamurias("Before Add::");
            AddSamuria();
            GetSamurias("After Add::");
            Console.WriteLine("Press any key");
            Console.ReadLine();
            
        }

        
        private static SamuriaContext Context = new SamuriaContext();
        
        private static void AddSamuria()
        {
            var samuria = new Samurai { Name = "Hyndi" };
            Context.Add(samuria);
            Context.SaveChanges();
        }

        private static void GetSamurias(string text)
        {
            var samurias = Context.Samurais.ToList();
            Console.WriteLine($"{text}:Samuria count is {samurias.Count}");
            samurias.ForEach(x => Console.WriteLine(x.Name));
        }
    }
}
