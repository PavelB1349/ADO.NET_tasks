using System.Data.Entity;
using System.Linq;

namespace AdoNetExamApp.Infrastructure
{
    internal abstract class BaseCommand : ICommand
    {
        protected BaseCommand(DbContext dataContext, ILogger logger)
        {
            DataContext = dataContext;
            Logger = logger;
        }

        protected DbContext DataContext { get; }
        protected ILogger Logger { get; }

        public abstract void Execute();

        protected IQueryable<T> Q<T>() where T : class
        {
            return DataContext.Set<T>().AsNoTracking();
        }
    }
}
