using SmartFinanceAI.Blazor.Models;
using System.Diagnostics;

namespace SmartFinanceAI.Blazor.Services;

public class PerformanceLogger
{
    private readonly string _logFilePath;
    private readonly double _cpuPowerWatts;

    public PerformanceLogger()
    {
        // CPU power obtained from an environment variable or set to the default value (65W)
        string? envPower = Environment.GetEnvironmentVariable("KBS_CPU_POWER_WATTS");
        if (!double.TryParse(envPower, out _cpuPowerWatts))
        {
            _cpuPowerWatts = 65; // Default value applies when no value is defined or when the input is invalid.
        }

        // Read environment variable or use default value
        string? envPath = Environment.GetEnvironmentVariable("KBS_LOG_PATH");

        if (string.IsNullOrEmpty(envPath))
        {
            Console.WriteLine("The environment variable 'KBS_LOG_PATH' is not set. Using the default file in the current directory.");
            envPath = Directory.GetCurrentDirectory();
        }

        // Validate if the path ends with '\', if not, add it
        if (!envPath.EndsWith('\\'))
        {
            envPath += "\\";
        }

        // Validate if the folder exists
        if (!Directory.Exists(envPath))
        {
            Console.WriteLine($"Error: The folder specified in 'KBS_LOG_PATH' does not exist: {envPath}");
            Directory.CreateDirectory(envPath);
        }

        // Build the full file path
        _logFilePath = $"{envPath}metrics.csv";

        // Create the file if it does not exist
        if (!File.Exists(_logFilePath))
        {
            File.WriteAllText(_logFilePath, InferenceMetrics.CsvHeader() + "\n");
        }
    }

    public InferenceMetrics Measure(Action inferenceAction)
    {
        Process proc = Process.GetCurrentProcess();
        var cpuStart = proc.TotalProcessorTime;
        var sw = Stopwatch.StartNew();

        inferenceAction.Invoke();

        sw.Stop();
        proc.Refresh();
        var cpuEnd = proc.TotalProcessorTime;

        var cpuTimeMs = (cpuEnd - cpuStart).TotalMilliseconds;

        var metrics = new InferenceMetrics
        {
            SessionId = Guid.NewGuid().ToString(),
            Timestamp = DateTime.Now,
            InferenceTimeMs = sw.ElapsedMilliseconds,
            RamMB = proc.PrivateMemorySize64 / 1024.0 / 1024.0,
            CpuTimeMs = cpuTimeMs,
            EstimatedEnergyJ = _cpuPowerWatts * (cpuTimeMs / 1000.0)
        };

        return metrics;
    }

    public void LogMetrics(InferenceMetrics metrics)
    {
        File.AppendAllText(_logFilePath, metrics.ToString() + "\n");
        Console.WriteLine($"[Sesión: {metrics.SessionId}] Métricas registradas:");
        Console.WriteLine(metrics.ToString());
    }

    public void LogMetrics(Action inferenceAction)
    {
        var metrics = Measure(inferenceAction);
        LogMetrics(metrics);
    }
}
