namespace SmartFinanceAI.Blazor.Models;

public class InferenceMetrics
{
    public required string SessionId { get; set; }
    public DateTime Timestamp { get; set; }
    public double InferenceTimeMs { get; set; }
    public double RamMB { get; set; }
    public double CpuTimeMs { get; set; }
    public double CpuUsagePercent { get; set; }
    public double EstimatedEnergyJ { get; set; }

    public override string ToString()
    {
        return $"{SessionId};{Timestamp:s};{InferenceTimeMs};{RamMB:F2};{CpuTimeMs:F2};{CpuUsagePercent:F2};{EstimatedEnergyJ:F2}";
    }

    public static string CsvHeader()
    {
        return "SessionId;Timestamp;InferenceTimeMs;RAM_MB;CPUTimeMs;CPUUsagePercent;EstimatedEnergyJ";
    }
}
