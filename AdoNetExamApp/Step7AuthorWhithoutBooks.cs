using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AdoNetExamApp.Data;
using AdoNetExamApp.Infrastructure;


//7) Вывести автора, который не написал ни одной книги

namespace AdoNetExamApp
{
    internal class Step7AuthorWhithoutBooks : BaseCommand
    {
        public Step7AuthorWhithoutBooks(DbContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }
        public override void Execute()
        {        
            var authorsAndBook = from a in Q<Author>()
                                 join b in Q<Book>()
                                 on a.Id equals b.AuthorId into ab
                                 from b in ab.DefaultIfEmpty()
                                 where b.Title == null

                                 group new { a, b } by new { a.LastName, a.FirstName, } into ab
                                 select new
                                 {
                                     FirstName = ab.Key.FirstName,
                                     LastName = ab.Key.LastName,                                     
                                 };

            var result = authorsAndBook.Select(x => new { x.LastName, x.FirstName }).First();

            Logger.Info($" Плохой автор :\t{result.LastName} {result.FirstName}.");
        }

    }
}