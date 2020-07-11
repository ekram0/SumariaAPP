using EFCore.Data;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Context.Database.EnsureCreated();
            //GetSamurias("Before Add::");
            //AddSamuria();
            //GetSamurias("After Add::");
            //InsertMultipleSamurais();
            //QueryFilter();
            //RetrivingAndUpdateSamurai();
            //RetrivingAndUpdateMultiSamurais();
            //InsertBattle();
            QueryUpdateBattle_Disconnected();
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

        private static void InsertMultipleSamurais()
        {
            var samuria = new Samurai { Name = "Sampson" };
            var samuria2 = new Samurai { Name = "Julia" };
            var samuria3 = new Samurai { Name = "Julia" };
            var samuria4 = new Samurai { Name = "Julia" };
            Context.Samurais.AddRange(samuria, samuria2, samuria3);
            Context.SaveChanges();
        }

        private static void QueryFilter()
        {
            //Find()=>Execute imediatly, if key is found in change tracker AVOID unneeded database Query.
            var samurias = Context.Samurais.Find(2);

            //var samurias2 = Context.Samurais.OrderBy(x => x.Name).ToList();

            //var samurias = Context.Samurais.FirstOrDefault(x => x.Name =="Sampson");

            //var samurias = Context.Samurais.Where(x => EF.Functions.Like(x.Name, "%j%")).ToList();
            //var samurias = Context.Samurais.Where(x => x.Name.Contains("Samp")).ToList();

            //var samurias = Context.Samurais.OrderBy(x => x.Id).LastOrDefault(); // order by desc
            //var samurias = Context.Samurais.OrderByDescending(x => x.Id).Select(s=>s.Name ).ToList(); //select case
            //var samurias = Context.Samurais.OrderByDescending(x => x.Id).Select(s=>s.Name == "Sampson").ToList();
            //var samurias = Context.Samurais.Where(s => s.Name == "Sampson").OrderByDescending(x => x.Id).ToList();

        }

        private static void RetrivingAndUpdateSamurai()
        {
            var samuria = Context.Samurais.FirstOrDefault();
            samuria.Name += "San";
            Context.SaveChanges();
        }
        private static void RetrivingAndUpdateMultiSamurais()
        {
            var samurias = Context.Samurais.Skip(1).Take(3).ToList();
            samurias.ForEach(s => s.Name += " San");
            Context.SaveChanges();
        }

        private static void InsertBattle()
        {
            Context.Battles.Add( new Battle { 
                Name="Battle of Okehzma",
                StartDate = new DateTime(1560,05,01),
                EndDate = new DateTime(1560,06,15),
            });
            Context.SaveChanges();
        }

        private static void QueryUpdateBattle_Disconnected()
        {
            var battle = Context.Battles.AsNoTracking().FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 30);
            using var newContext = new SamuriaContext();
            newContext.Battles.Update(battle);
            newContext.SaveChanges();
        }
    }

}
