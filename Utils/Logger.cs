using Back.Models;
using Back.Utils;
using Serilog;

namespace front.Utils.Logger
{
    public class Logger
    {
        private static readonly object _lock = new object();
        private static Logger _instance;

        public Logger()
        {
            string storagePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Logs\\Logs.txt");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(storagePath, rollingInterval: RollingInterval.Day, shared: true)
                .CreateLogger();
        }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = AOPFactory.CreateService<Logger>(typeof(Logger));
                        }
                    }
                }
                return _instance;
            }
        }

        public virtual void Information(string message, int? user = null) =>
            Log.Information($"By:{user}\tAction:{message}");

        public virtual void Warning(string message, int? user = null) =>
            Log.Warning(user == null ? $"Action:{message}" : $"By:{user}\tAction:{message}");

        public virtual void Error(string message, int? user = null) =>
            Log.Error($"By:{user}\tAction:{message}");
    }

}
