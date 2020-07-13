using EFCore.Data;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public class BusinessDataLogic
    {
        private SamuriaContext _context;

        public BusinessDataLogic(SamuriaContext context)
        {
            _context = context;
        }

        public BusinessDataLogic()
        {
            _context = new SamuriaContext();
        }

        public int AddMultipleSamurais(string[] nameList)
        {
            var samuraiList = new List<Samurai>();
            foreach (var name in nameList)
            {
                samuraiList.Add(new Samurai { Name = name });
            }
            _context.Samurais.AddRange(samuraiList);

            var dbResult = _context.SaveChanges();
            return dbResult;
        }

        public int InsertNewSamurai(Samurai samurai)
        {
            _context.Samurais.Add(samurai);
            var dbResult = _context.SaveChanges();
            return dbResult;
        }


        public Samurai GetSamuraiWithQuotes(int samuraiId)
        {
            var samuraiWithQuotes = _context.Samurais.Where(s => s.Id == samuraiId)
                                                     .Include(s => s.Quotes)
                                                     .FirstOrDefault();

            return samuraiWithQuotes;
        }

    }
}