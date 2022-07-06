using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AdoNetExamApp.Data;
using AdoNetExamApp.Infrastructure;

//6) Вывести автора с наибольшим количеством страниц в книгах

namespace AdoNetExamApp
{
    internal class Step6AuthorWhithMaxPagesInBooks : BaseCommand
    {
        public Step6AuthorWhithMaxPagesInBooks(DbContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }
        public override void Execute()
        {
            var authorsAndBooks = from a in Q<Author>()
                                  join b in Q<Book>()
                                  on a.Id equals b.AuthorId
                                  orderby b.Pages descending                                  
                                  select new { a.FirstName, a.LastName, b.Pages, b.Title };

            var result = authorsAndBooks.First();
                        
            Logger.Info($"Наибольшее количество страниц у автора:\t{result.LastName} {result.FirstName}. Книга: {result.Title}, количество страниц: {result.Pages}.");            
        }
    }
}