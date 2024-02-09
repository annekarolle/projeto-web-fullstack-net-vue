using System.Collections.Concurrent;

namespace Orbita.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly CustomLoggerProviderConfiguration _loggerConfig;
        private readonly ConcurrentDictionary<string, CustomLogger> Loggers = new ConcurrentDictionary<string, CustomLogger>();

        public CustomLoggerProvider(CustomLoggerProviderConfiguration loggerConfig)
        {
           _loggerConfig = loggerConfig;
        }
       public ILogger CreateLogger(string categoryName)
        {
            return (ILogger)Loggers.GetOrAdd(categoryName,
                name => new CustomLogger(name, _loggerConfig));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
