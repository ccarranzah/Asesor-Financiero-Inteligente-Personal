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
        _logFilePath = $"{envPath}inference_metrics.csv";

        // Create the file if it does not exist
        if (!File.Exists(_logFilePath))
        {
            File.WriteAllText(_logFilePath, InferenceMetrics.CsvHeader() + "\n");
        }
    }

    public InferenceMetrics Measure(NRules.ISession session, string sessionId)
    {
        const double BytesToMB = 1024.0 * 1024.0;
        var stopwatch = Stopwatch.StartNew();
        session.Fire();
        stopwatch.Stop();

        var process = Process.GetCurrentProcess();

        var metrics = new InferenceMetrics
        {
            SessionId = sessionId,
            Timestamp = DateTime.Now,
            InferenceTimeMs = stopwatch.ElapsedMilliseconds,
            RamMB = Math.Round(process.WorkingSet64 / BytesToMB, 2),
            CpuTimeMs = process.TotalProcessorTime.TotalMilliseconds,
            EstimatedEnergyJ = _cpuPowerWatts * stopwatch.Elapsed.TotalSeconds
        };

        return metrics;
    }

    public void LogMetrics(InferenceMetrics metrics)
    {
        File.AppendAllText(_logFilePath, metrics.ToString() + "\n");
        Console.WriteLine($"[Sesión: {metrics.SessionId}] Métricas registradas:");
        Console.WriteLine(metrics.ToString());
    }

    public void LogInference(NRules.ISession session, string sessionId)
    {
        var metrics = Measure(session, sessionId);
        LogMetrics(metrics);
    }
}
