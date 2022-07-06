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


//"8) Вывести автора, который написал больше всего книг
namespace AdoNetExamApp
{
    class Step8AuthorWhoHasWrittenTheMostBooks : BaseCommand
    {
        public Step8AuthorWhoHasWrittenTheMostBooks(DbContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }

        public override void Execute()
        {

            var query = from a in Q<Author>()
                                        join b in Q<Book>()
                                        on a.Id equals b.AuthorId
                                        group new { a, b } by new { a.LastName, a.FirstName } into ab
                                        orderby ab.Count() descending
                                        select new
                                        {
                                            FirstName = ab.Key.FirstName,
                                            LastName = ab.Key.LastName,
                                            Count = ab.Count(x => x.b.Title != null)
                                        };

            var result = query.Select(x => new { x.FirstName, x.LastName, x.Count }).First();

            Logger.Info($" Автор который написал больше всего книг: {result.FirstName} {result.LastName}; Книг у автора: {result.Count}");
        }
    }
}
