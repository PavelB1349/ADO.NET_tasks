using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AdoNetExamApp.Data;
using AdoNetExamApp.Infrastructure;


//5) Вывести названия двух первых самых дорогих книг

namespace AdoNetExamApp
{
    class Step5TwoMostExpensiveBooks : BaseCommand
    {
        public Step5TwoMostExpensiveBooks(DbContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }

        public override void Execute()
        {
            var books = from b in Q<Book>()
                        orderby b.Cost descending
                        
                        select new { b.Title, b.Cost };

            var result = books.Select(x => new { x.Title, x.Cost}).Take(2);

            foreach (var book in result)
            {
                Logger.Info($" название: {book.Title}  цена : {book.Cost} ");
            }
        }
    }
}
