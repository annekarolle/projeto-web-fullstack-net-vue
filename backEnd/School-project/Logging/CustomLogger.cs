namespace Orbita.Logging
{
    public class CustomLogger : ILogger
    {
        private string _loggerName;
        private CustomLoggerProviderConfiguration _loggerConfig;

        public CustomLogger(string name, CustomLoggerProviderConfiguration loggerConfig)
        {
            _loggerName = name;
            _loggerConfig = loggerConfig;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var mensage = string.Format($"{logLevel} : {eventId}" +
                                        $" - {formatter(state, exception)}");

            WriteTextInFile(mensage);

        }

        private void WriteTextInFile(string mensage)
        {
            var filePath = $@"C:\Users\annek\OneDrive\Documentos\ESTUDO\a+\orbita-challenge-full-stack-web\backEnd\bin\LOG-{DateTime.Now:yyyy-MM-dd}.txt";
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.Create(filePath).Dispose();
            }

            using StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine(mensage);
            writer.Close();
        }
    }
}
