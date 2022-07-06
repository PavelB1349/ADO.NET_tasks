using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AdoNetExamApp.Data;
using AdoNetExamApp.Infrastructure;
using System.Data.Entity;

//4) Вывести авторов самой дорогой и самой дешевой книги
namespace AdoNetExamApp


{
    class Step4AuthorsOfTheMostExpensiveAndCheapestBook : BaseCommand
    {
        public Step4AuthorsOfTheMostExpensiveAndCheapestBook(DbContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }
        public override void Execute()
        {
            var authorsAndBooks = from a in Q<Author>()
                                  join b in Q<Book>()
                                  on a.Id equals b.AuthorId
                                  group new { a, b } by new { a.LastName, a.FirstName, b.Cost } into ab
                                  orderby ab.Key.Cost
                                  select new
                                  {
                                      LastName = ab.Key.LastName,
                                      FirstName = ab.Key.FirstName,
                                      Cost = ab.Key.Cost
                                  };
            var minPrice = authorsAndBooks.Take(1).First(); 
                              
            var queryForMax = from a in Q<Author>()
                              join b in Q<Book>()
                              on a.Id equals b.AuthorId
                              group new { a, b } by new { a.LastName, a.FirstName, b.Cost } into ab
                              orderby ab.Key.Cost descending
                              select new
                              {
                                  LastName = ab.Key.LastName,
                                  FirstName = ab.Key.FirstName,
                                  Cost = ab.Key.Cost
                              };
            var maxPrice = queryForMax.Take(1).First();

            Logger.Info($" Автор самой дорогой книги:{maxPrice.LastName} {maxPrice.FirstName} цена: {maxPrice.Cost}");
            Logger.Info($" Автор самой дешёвой книги:{minPrice.LastName} {minPrice.FirstName} цена: {minPrice.Cost}");

        }
    }
}