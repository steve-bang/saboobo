
namespace SaBooBo.Domain.Shared.Utils;

/// <summary>
/// This class is used to write logs to the console.
/// And it will write the file name to the log message.
/// Share by all projects with the same namespace.
/// </summary>
public static class LoggingUtil
{
    public static void WriteLog(string message)
    {
        Console.WriteLine(CreateLogMessage(message));
    }
    

    public static void WriteLog(string message, string fileName)
    {
        Console.WriteLine(CreateLogMessage(message, fileName));
    }

    public static string CreateLogMessage(string message, string fileName)
    {
        return $"[x] [{DateTime.UtcNow}] {message} at file {fileName}";
    }
    public static string CreateLogMessage(string message)
    {
        return $"[x] [{DateTime.UtcNow}] {message}";
    }

    public static void WriteLog(string message, string fileName, Exception exception)
    {
        WriteLog(message);
        Console.WriteLine(CreateLogMessage(message, fileName));
        Console.WriteLine(exception.StackTrace);
    }

    public static string CreateLogMessage(string message, string fileName, Exception exception)
    {
        return $"[x] [{DateTime.UtcNow}] {message} at file {fileName}";
    }

    public static void WriteLog(Exception exception, string fileName)
    {
        WriteLog(exception.Message, fileName);
        Console.WriteLine(exception.StackTrace);
    }

    public static string CreateLogMessage(Exception exception, string fileName)
    {
        return $"[x] [{DateTime.UtcNow}] ERROR: {exception.Message} at file {fileName}";
    }

    public static void WriteLog(Exception exception)
    {
        Console.WriteLine(CreateLogMessage(exception));
        Console.WriteLine(exception.StackTrace);
    }

    public static string CreateLogMessage(Exception exception)
    {
        return $"[x] [{DateTime.UtcNow}] ERROR: {exception.Message}";
    }
}
