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



//2) Вывести стоимость книг в разрезе категории
namespace AdoNetExamApp
{
    class Step2LogPricesFromCategory : BaseCommand
    {
        public Step2LogPricesFromCategory(DbContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }

        public override void Execute()
        {
     
            var categories = from q in Q<Category>()
                             join b in Q<Book>() on q.Id equals b.CategoryId
                             select new { Category = q.Name, b.Cost,};

            var gropped = from c in categories
                          group c by c.Category into g
                          select new { Category = g.Key, Total = g.Sum(z => z.Cost) };

           

            foreach (var book in gropped)
            {
                Logger.Info($" Категория: {book.Category}.\tСумма стоимости книг: {book.Total} ");
            }
            Console.WriteLine("Если быть точнее:");

            /// вроде это по заданию надо
            var query = from c in Q<Category>()
                        join b in Q<Book>() on c.Id equals b.CategoryId
                        orderby b.CategoryId
                        select new { Category = c.Name, b.Title, b.Cost };           

            foreach (var book in query)
            {
                Logger.Info($" Категория: {book.Category};\tНазвание книги: {book.Title};\tЦена книги: {book.Cost}.");
            }
        }

    }
}
