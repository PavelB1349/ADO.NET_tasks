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


//Вывести всех авторов, которые пишут в выбранном жанре(жанр ввести)
namespace AdoNetExamApp
{
    class Step3SelectAllAuthorsByCategory : BaseCommand
    {
        public Step3SelectAllAuthorsByCategory(DbContext dataContext, ILogger logger) : base(dataContext, logger)
        {
        }

        public override void Execute()
        {

            Console.WriteLine("\tУкажите категоррию книги:");
            var userInput = Console.ReadLine();

            if (!Valide(userInput, Q<Category>()))
            {
                Logger.Error("Категория  указана не корректно!");
                return;
            }          

            var id = Q<Category>().Where(x => x.Name == userInput).Select(x => x.Id).FirstOrDefault();

            var query = from a in Q<Author>()
                       join b in Q<Book>()
                       on a.Id equals b.AuthorId
                       where b.CategoryId == id
                       select new { a.LastName, a.FirstName };

            var result = query.Distinct();

            foreach (var author in result)
            {
                Logger.Info($"Автор: {author.LastName} {author.FirstName}.\tЖанр: {userInput}.");
            }
        }
        private bool Valide(string userInput, IQueryable<Category> category)
        {
            if (string.IsNullOrEmpty(userInput))
            {
                return false;
            }
            if (!category.Where(x => x.Name.Contains(userInput)).Any())
            {
                return false;
            }
            return true;
        }
    }
}
