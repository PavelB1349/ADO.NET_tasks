using System;
using System.IO;

namespace AdoNetExamApp.Infrastructure
{
    internal class ConsoleLogger : TextLogger
    {
        private static readonly Lazy<ConsoleLogger> lazInstance =
            new Lazy<ConsoleLogger>(() => new ConsoleLogger());

        public static ConsoleLogger Instance => lazInstance.Value;

        private readonly ConsoleColor defaultFontColor;

        private ConsoleLogger(bool autoFlush = true) : base(Console.Out, autoFlush)
        {
            this.defaultFontColor = Console.ForegroundColor;
        }

        public override void Error(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            base.Error(message, args);
            Console.ForegroundColor = defaultFontColor;
        }

        public override void Fatal(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            base.Fatal(message, args);
            Console.ForegroundColor = defaultFontColor;
        }

        public override void Info(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            base.Info(message, args);
            Console.ForegroundColor = defaultFontColor;
        }

        public override void Warn(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            base.Warn(message, args);
            Console.ForegroundColor = defaultFontColor;
        }
    }

    internal abstract class TextLogger : ILogger
    {
        private readonly TextWriter writer;
        private readonly bool autoFlush;

        protected TextLogger(TextWriter writer, bool autoFlush)
        {
            this.writer = writer;
            this.autoFlush = autoFlush;
        }

        public virtual void Error(string message, params object[] args)
            => Log("ERROR", message, args);

        public virtual void Fatal(string message, params object[] args)
            => Log("FATAL", message, args);

        public virtual void Info(string message, params object[] args)
            => Log("INFO", message, args);

        public virtual void Warn(string message, params object[] args)
            => Log("WARN", message, args);

        protected virtual string FormatMessage(string level, string message, object[] arguments)
        {
            string formattedMessage = string.Format(message, arguments);

            return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{level}\t{formattedMessage}";
        }

        private void Log(string level, string message, object[] arguments)
        {
            writer.WriteLine(FormatMessage(level, message, arguments));

            if (autoFlush)
            {
                writer.Flush();
            }
        }
    }
    
    internal interface ILogger
    {
        void Info(string message, params object[] args);
        void Warn(string message, params object[] args);

        void Error(string message, params object[] args);
        void Fatal(string message, params object[] args);
    }
}
