using System;
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Logger.Logger;

public class FileLogger : ILogger
{
	private readonly string _categoryName;

	private readonly string _filePath;

	private readonly ConcurrentQueue<string> _logMesssages;

	public FileLogger(string categoryName, string filePath, ConcurrentQueue<string> logMessages)
	{
		_categoryName = categoryName;
		_filePath = filePath;
		_logMesssages = logMessages;
	}

	public IDisposable BeginScope<TState>(TState state)
	{
		return null;
	}

	public bool IsEnabled(LogLevel logLevel)
	{
		return logLevel >= LogLevel.Information;
	}

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        string value = formatter(state, exception);
        string text = $"{DateTime.UtcNow:O} [{logLevel}] {_categoryName}: {value}";
        if (exception != null)
        {
            text += Environment.NewLine + exception;
        }

        _logMesssages.Enqueue(text);
    }
}
