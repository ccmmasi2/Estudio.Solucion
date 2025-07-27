using Estudio.Application.Interface;

namespace Estudio.Infrastructure.Logging
{
    public class TraceLogger : ITraceLogger
    {
        public void Log(string message)
        {
            File.AppendAllText("trace.log", $"{DateTime.Now}: {message}\n");
        }
    }
}
