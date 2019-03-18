using BookLib.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookLib.Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-BookLib-4E6ABAD7-8749-4CE7-B8FE-3C6CFCF09A4C;Trusted_Connection=True;MultipleActiveResultSets=true").Options;
            using (ApplicationDbContext context = new ApplicationDbContext(options))
            {
                context.Book.ToList().ForEach(b => Console.WriteLine(b.Name));
            }
        }
    }
}
