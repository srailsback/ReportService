using System.ComponentModel;

/// <summary>
/// Report Format Enum
/// </summary>
/// <remarks>Per project specs, only provider support for PDF and Excel</remarks>
public enum ReportFormat
{
    [Description("application/pdf")]
    PDF,
    [Description("application/vnd.ms-excel")]
    Excel
}
