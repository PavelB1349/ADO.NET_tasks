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


//9) Вывести автора и среднее количество страниц в его книгах
namespace AdoNetExamApp
{
    class Step9AuthorAndAverageNumberOfPages : BaseCommand
    {
        public Step9AuthorAndAverageNumberOfPages(DbContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }
        public override void Execute()
        {            
            var authorsAndPages = from a in Q<Author>()
                           join b in Q<Book>()
                           on a.Id equals b.AuthorId
                           group new { a, b } by new { a.LastName, a.FirstName } into ab
                           select new
                           {
                               LastName = ab.Key.LastName,
                               FirstName = ab.Key.FirstName,
                               Avr = ab.Average(x => x.b.Pages)                               
                           };

            foreach (var avr in authorsAndPages)
            {
                Logger.Info($"Атор: {avr.LastName} {avr.FirstName}, среднее количество страниц: {avr.Avr}");
            }
        }
       
}
}
