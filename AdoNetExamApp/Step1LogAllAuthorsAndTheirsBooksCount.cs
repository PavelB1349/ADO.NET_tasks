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


// 1) Вывести всех авторов книг и количество книг, им написанных"
namespace AdoNetExamApp
{
    internal class Step1LogAllAuthorsAndTheirsBooksCount : BaseCommand
    {
        public Step1LogAllAuthorsAndTheirsBooksCount(DbContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }

        public override void Execute()
        {
            var allAuthors = Q<Author>()
                .Include(x => x.Books)
                .Select(x => new { x.LastName, x.FirstName, x.Books })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName);

            var authors = from a in Q<Author>()
                          join b in Q<Book>()
                          on a.Id equals b.AuthorId
                          group new { a, b } by new { a.LastName, a.FirstName, b.AuthorId } into ab
                          select new
                          {
                              FirstName = ab.Key.FirstName,
                              LastName = ab.Key.LastName,
                              CountBooks = ab.Count(x => x.b.AuthorId != null)
                          };
            var result = authors.Select(x => new { x.LastName, x.FirstName, x.CountBooks });

            foreach (var author in result)
            {
                Logger.Info($"Автор: {author.LastName} {author.FirstName}. Количество книг: {author.CountBooks}.");
            }
        }
    }
}