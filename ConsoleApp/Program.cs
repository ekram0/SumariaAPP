using EFCore.Data;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
            //QueryUpdateBattle_Disconnected();
            //InsertNewSamuraiWithQuote();
            //AddQuoteToExistingSamuraiWihileTracked();
            //AddQuoteToExistingSamuraiNotTracked(2);
            //AddQuoteToExistingSamuraiNotTracked_Easy(2);
            //EaggerLoadingSamuraiWithQuotes();
            //projectSomeProperties();
            //ExplicitLoadingQuotes();
            //FilteringWiththeRelatedData();
            //ModifyingRelatedDataWithTracked();
            //ModifyingRelatedDataWithNotTracked();
            //JoinBattleAndSamurai();
            //EnlistSamuariIntoBattle();
            //RemoveJoinBetweenSamuraiAndBattleSimple();
            //GetSamuraiWithBattle();
            //AddNewHorseToSamuraiUsingID();
            //AddNewHorseToSamuraiUsingObject();
            //AddNewHorseToDisconnectedSamuraiObject();
            //ReplaceAHorse();
            //GetSamuraiwithHorse();
            //GetHorseWithSamurai();
            //GetClanWithSamurais();
            //QuerySamuraiBattleStats();
            //queryUsingRawSql();
            //queryUsingRawSqlStoreProcedure();
            ExecuteSomeRaqSql();
            Console.WriteLine("Press any key");
            Console.ReadLine();

        }

        public struct IdAndName
        {
            private readonly string name;
            public int Id { get; }
            public IdAndName(int id, string name)
            {
                Id = id;
                this.name = name;
            }

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
            Context.Battles.Add(new Battle
            {
                Name = "Battle of Okehzma",
                StartDate = new DateTime(1560, 05, 01),
                EndDate = new DateTime(1560, 06, 15),
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

        private static void InsertNewSamuraiWithQuote()
        {
            var samuria = new Samurai
            {
                Name = "Snowden",
                Quotes = new List<Quote>
                {
                    new Quote{ Text = "Watch out for my sharp sword!"},
                    new Quote{ Text = "Watch out for my sharp sword!"},
                    new Quote{ Text = "I told you to watch out for the sharp sword! oh well1"},
                    new Quote{ Text = "I told you to watch out for the sharp sword! oh well1"},
                    new Quote{ Text = "I told you to watch out for the sharp sword! oh well1"}
                }
            };
            Context.Samurais.Add(samuria);
            Context.SaveChanges();
        }

        private static void AddQuoteToExistingSamuraiWihileTracked()
        {
            var samuria = Context.Samurais.FirstOrDefault();
            samuria.Quotes.Add(new Quote
            {
                Text = "I bet you're happy that i've saved you"
            });
            Context.SaveChanges();
        }

        private static void AddQuoteToExistingSamuraiNotTracked(int samuriaID)
        {
            var samuria = Context.Samurais.Find(samuriaID);
            samuria.Quotes.Add(new Quote
            {
                Text = "Now that i saved you, will you feed me dinner?"
            });

            using SamuriaContext newContext = new SamuriaContext();
            newContext.Samurais.Update(samuria);
            newContext.SaveChanges();
        }

        private static void AddQuoteToExistingSamuraiNotTracked_Easy(int id)
        {
            var quote = new Quote
            {
                Text = "Now that i saved you, will you feed me dinner again?",
                SamuraiID = id

            };
            using SamuriaContext newContext = new SamuriaContext();
            newContext.Quotes.Add(quote);
            newContext.SaveChanges();
        }

        private static void EaggerLoadingSamuraiWithQuotes()
        {
            var samuraiWithQuotes = Context.Samurais
                .Include(s => s.Quotes)
                .ToList();

            var samuariWitClans = Context.Samurais
                .Include(s => s.Clan)
                .Include(c => c.Quotes)
                .ThenInclude(q => q.Text).ToList();

        }

        private static void projectSomeProperties()
        {
            //var someProjection = Context.Samurais
            //    .Select(s => new { s.Id, s.Name }).ToList();

            //var idAndName = Context.Samurais
            //    .Select(x=>new IdAndName(x.Id , x.Name)).ToList();

            var samuraiHappyQuote = Context.Samurais
                 .Select(s => new
                 {
                     samuria = s,
                     HappyQuote = s.Quotes.Where(q => q.Text.Contains("happy"))
                 }).ToList();

            Console.WriteLine("------------------------");

            var happyQuotes = samuraiHappyQuote[0].samuria.Name += "The Happiest";

            Context.SaveChanges();

        }

        private static void ExplicitLoadingQuotes()
        {
            var samurai = Context.Samurais
                .FirstOrDefault(s => s.Name.Contains("Hyndi"));
            Console.WriteLine("------------");
            Context.Entry(samurai).Collection(s => s.Quotes).Load();
            Console.WriteLine("------------");
            Context.Entry(samurai).Reference(s => s.Horse).Load();
        }

        private static void FilteringWiththeRelatedData()
        {
            var samuria = Context.Samurais
                .Where(s => s.Quotes.Any(q => q.Text.Contains("happy")))
                .ToList();

        }

        private static void ModifyingRelatedDataWithTracked()
        {
            var samuria = Context.Samurais
                .Include(s => s.Quotes)
                .FirstOrDefault(s => s.Id == 2);

            samuria.Quotes[0].Text = "Did you hear that";
            Context.SaveChanges();
        }

        private static void ModifyingRelatedDataWithNotTracked()
        {
            var quote = Context.Samurais
                .Include(s => s.Quotes)
                .FirstOrDefault(s => s.Id == 2).Quotes[0];

            //var quote = samuria.Quotes[0];

            quote.Text += "Did you hear that again again?";

            using var newContext = new SamuriaContext();
            newContext.Entry(quote).State =EntityState.Modified;
            newContext.SaveChanges();

        }

        private static void JoinBattleAndSamurai()
        {
            var sbJoin = new SamuraiBattle { BattleID =1 , SamuriaID=1 };
            Context.Add(sbJoin);
            Context.SaveChanges();
        }

        private static void EnlistSamuariIntoBattle()
        {
            var battle = Context.Battles.Find(1);
            battle.SamuraiBattles.Add(new SamuraiBattle { BattleID=1});
            Context.SaveChanges();
        }

        private static void RemoveJoinBetweenSamuraiAndBattleSimple()
        {
            var join = new SamuraiBattle { SamuriaID = 1, BattleID = 1 };
            Context.Remove(join);
            Context.SaveChanges();
        }

        private static void GetSamuraiWithBattle()
        {
            var samuraiWithBattle = Context.Samurais
                .Include(s => s.SamuraiBattles)
                .ThenInclude(b => b.Battle)
                .FirstOrDefault(sm => sm.Id == 2);
            Console.WriteLine("-----------------");
            var samuraiWithBattlesCleaner = Context.Samurais
                .Where(s => s.Id == 2)
                .Select(p => new
                {
                    samurai = p,
                    battle = p.SamuraiBattles.Select(sb => sb.Battle)
                })
                .FirstOrDefault();
        }

        public static void AddNewHorseToSamuraiUsingID()
        {
            var horse = new Horse { Name = "Scout", SamuriaId = 2 };
            Context.Add(horse);
            Context.SaveChanges();
        }

        public static void AddNewHorseToSamuraiUsingObject()
        {
            var samuria = Context.Samurais.Find(3);
            samuria.Horse = new Horse { Name = "Black Beauty" };
            Context.SaveChanges();
        }

        private static void AddNewHorseToDisconnectedSamuraiObject()
        {

            var samurai = Context.Samurais.AsNoTracking()
                .FirstOrDefault(s => s.Id == 4);

            samurai.Horse = new Horse { Name = "Mr. sa" };

            using var newContext = new SamuriaContext();
            newContext.Attach(samurai); 
            newContext.SaveChanges();
        }

        public static void ReplaceAHorse()
        {
            //var samurai = Context.Samurais
            //    .Include(s => s.Horse)
            //    .FirstOrDefault(s => s.Id == 4);

            var samurai = Context.Samurais.Find(4);
            samurai.Horse = new Horse { Name = "Trigger" };
            Context.SaveChanges();
        }

        public static void GetSamuraiwithHorse()
        {
            var Samurai = Context.Samurais.Include(s => s.Horse).ToList();
        }

        public static void GetHorseWithSamurai()
        {
            var horseWithSamurai = Context.Set<Horse>().Find(3);

            var horseWithoutSamurai = Context.Samurais
                .Include(s => s.Horse).FirstOrDefault(s => s.Horse.Id == 3);

            var horseWithSamurais = Context.Samurais
                .Where(s => s.Horse != null)
                .Select(s => new { Horse = s.Horse, Samurai = s })
                .ToList();
            
        }

        public static void GetSamuraiWithClan()
        {
            var samurai = Context.Samurais
                .Include(s => s.Clan).FirstOrDefault();
        }

        public static void GetClanWithSamurais()
        {
            var clan = Context.Clans.Find(3);
            var samuraiForClan = Context.Samurais.Where(s => s.Clan.Id == 3).ToList();

        }

        public static void QuerySamuraiBattleStats()
        {
            //var stats = Context.SamuraiBattleStats.ToList();

            var firstStat = Context.SamuraiBattleStats.FirstOrDefault();

            //var sampsonStat = Context.SamuraiBattleStats
            //    .Where(s => s.Name == "Sampson").FirstOrDefault();
        }


        public static void queryUsingRawSql()
        {
            //var samuria = Context.Samurais.FromSqlRaw("select * from Samurais").ToList();

            var samurias = Context.Samurais
                .FromSqlRaw("select * from Samurais")
                .Include(s=>s.Quotes).ToList();
        }

        public static void queryUsingRawSqlStoreProcedure()
        {
            var text = "Happy";
            var samurais = Context.Samurais
                .FromSqlRaw($"EXEC SamuraisWhoSaidAWord {text}").ToList();
        }

        public static void ExecuteSomeRaqSql()
        {
            var samuraiOd = 22;
            var x = Context.Database
                .ExecuteSqlRaw($"EXEC DeleteQuotesForSamurai {samuraiOd}");

        }

    }



}
