using AdoNetExamApp.Data;
using AdoNetExamApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetExamApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (DbContext db = new ExamContext())
            {
                Console.WriteLine(" 1) Вывести всех авторов книг и количество книг, им написанных"); 
                Execute<Step1LogAllAuthorsAndTheirsBooksCount>(db);                
                Console.WriteLine("");

                Console.WriteLine("2) Вывести стоимость книг в разрезе категории");
                Execute<Step2LogPricesFromCategory>(db);
                Console.WriteLine("");

                Console.WriteLine("3) Вывести всех авторов, которые пишут в выбранном жанре(жанр ввести)");
                Execute<Step3SelectAllAuthorsByCategory>(db);
                Console.WriteLine();

                Console.WriteLine(" 4) Вывести авторов самой дорогой и самой дешевой книги");
                Execute<Step4AuthorsOfTheMostExpensiveAndCheapestBook>(db);
                Console.WriteLine();

                Console.WriteLine(" 5) Вывести названия двух первых самых дорогих книг");
                Execute<Step5TwoMostExpensiveBooks>(db);
                Console.WriteLine();

                Console.WriteLine("6) Вывести автора с наибольшим количеством страниц в книгах");
                Execute<Step6AuthorWhithMaxPagesInBooks>(db);
                Console.WriteLine();

                Console.WriteLine("7) Вывести автора, который не написал ни одной книги");
                Execute<Step7AuthorWhithoutBooks>(db);
                Console.WriteLine();

                Console.WriteLine("8) Вывести автора, который написал больше всего книг");
                Execute<Step8AuthorWhoHasWrittenTheMostBooks>(db);
                Console.WriteLine();

                Console.WriteLine("9) Вывести автора и среднее количество страниц в его книгах");
                Execute<Step9AuthorAndAverageNumberOfPages>(db);
                
            }
            Console.WriteLine("done, press any key to continue...");
            Console.ReadLine();
        }

        private static void Execute<TCommand>(DbContext db)
           where TCommand : class, ICommand
        {
            var command = (ICommand)
                Activator.CreateInstance(typeof(TCommand), new object[] { db, ConsoleLogger.Instance });

            command.Execute();
        }
    }
}
