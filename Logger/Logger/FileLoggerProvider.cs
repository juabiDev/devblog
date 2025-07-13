using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Logger.Logger;

public class FileLoggerProvider : ILoggerProvider, IDisposable
{
	private readonly string _filePath;
	private ConcurrentQueue<string> _logMessages;
    private readonly CancellationTokenSource _cts = new();
    private readonly Task _logProcessorTask;

    public FileLoggerProvider(string filePath)
	{
		_filePath = filePath;
		_logMessages = new ConcurrentQueue<string>();
        _logProcessorTask = Task.Run(() => ProcessLogQueueAsync(_cts.Token));
	}

	public ILogger CreateLogger(string categoryName)
	{
		return new FileLogger(categoryName, _filePath, _logMessages);
	}

	public void Dispose()
	{
        _cts.Cancel();
        _logProcessorTask.Wait(); // Espera a que termine
        _cts.Dispose();
    }

    private async Task ProcessLogQueueAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (!_logMessages.IsEmpty)
            {
                var directory = Path.GetDirectoryName(_filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using var stream = new FileStream(_filePath, FileMode.Append, FileAccess.Write, FileShare.Read);
                using var writer = new StreamWriter(stream);

                while (_logMessages.TryDequeue(out var message))
                {
                    writer.WriteLine(message);
                }
            }

            await Task.Delay(1000, token);
        }
    }
}
