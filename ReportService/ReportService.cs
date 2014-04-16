using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Routing;

/// <summary>
/// Report Service
/// </summary>
public class ReportService
{
    private ReportServiceCredential Credentials { get; set; }
    private WebClient WebClient { get; set; }


    public ReportService()
    {
    }

    /// <summary>
    /// Report Service
    /// </summary>
    /// <param name="reportPath">Path of report as defined in SSRS</param>
    /// <param name="reportFormat">Report format enum vale</param>
    /// <remarks>Downloads report using provided report path</remarks>
    public ReportService(string reportPath, ReportFormat reportFormat)
    {
        this.Credentials = new ReportServiceCredential();
        this.WebClient = this.Credentials.GetClient();
        DownloadReport(reportPath, reportFormat, "", null);
    }

    /// <summary>
    /// Report Service
    /// </summary>
    /// <param name="reportPath">Path of report as defined in SSRS</param>
    /// <param name="reportFormat">Report format enum vale</param>
    /// <param name="reportFileName">Desired file name of downloaded report</param>
    /// <remarks>Downloads report using provided report path</remarks>
    public ReportService(string reportPath, ReportFormat reportFormat, string reportFileName)
    {
        this.Credentials = new ReportServiceCredential();
        this.WebClient = this.Credentials.GetClient();
        DownloadReport(reportPath, reportFormat, reportFileName, null);
    }

    /// <summary>
    /// Report Service
    /// </summary>
    /// <param name="reportPath">Path of report as defined in SSRS</param>
    /// <param name="reportFormat">Report format enum vale</param>
    /// <param name="reportFileName">Desired file name of downloaded report</param>
    /// <param name="parameters">Anonymous object type of report parameters</param>
    /// <remarks>Downloads report using provided report path</remarks>
    public ReportService(string reportPath, ReportFormat reportFormat, string reportFileName, object parameters)
    {
        this.Credentials = new ReportServiceCredential();
        this.WebClient = this.Credentials.GetClient();
        DownloadReport(reportPath, reportFormat, reportFileName, parameters);
    }


    /// <summary>
    /// Report Service
    /// </summary>
    /// <param name="reportType">Report type, this is the folder name is SSRS</param>
    /// <param name="reportName">Report name, the name of report in SSRS</param>
    /// <param name="reportFormat">Desired report format enum</param>
    /// <remarks>Downloads report using provided report type and name</remarks>
    public ReportService(string reportType, string reportName, ReportFormat reportFormat)
    {
        this.Credentials = new ReportServiceCredential();
        this.WebClient = this.Credentials.GetClient();
        DownloadReport(GetReportPath(reportType, reportName), reportFormat, "", null);
    }

    /// <summary>
    /// Report Service
    /// </summary>
    /// <param name="reportType">Report type, this is the folder name is SSRS</param>
    /// <param name="reportName">Report name, the name of report in SSRS</param>
    /// <param name="reportFormat">Desired report format enum</param>
    /// <param name="reportFileName">Desired file name of downloaded report</param>
    /// <remarks>Downloads report using provided report type and name</remarks>
    public ReportService(string reportType, string reportName, ReportFormat reportFormat, string reportFileName)
    {
        this.Credentials = new ReportServiceCredential();
        this.WebClient = this.Credentials.GetClient();
        DownloadReport(GetReportPath(reportType, reportName), reportFormat, reportFileName, null);
    }

    /// Report Service
    /// </summary>
    /// <param name="reportType">Report type, this is the folder name is SSRS</param>
    /// <param name="reportName">Report name, the name of report in SSRS</param>
    /// <param name="reportFormat">Desired report format enum</param>
    /// <param name="reportFileName">Desired file name of downloaded report</param>
    /// <param name="parameters">Anonymous object type of report parameters</param>
    /// <remarks>Downloads report using provided report type and name</remarks>
    public ReportService(string reportType, string reportName, ReportFormat reportFormat, string reportFileName, object parameters)
    {
        this.Credentials = new ReportServiceCredential();
        this.WebClient = this.Credentials.GetClient();
        DownloadReport(GetReportPath(reportType, reportName), reportFormat, reportFileName, parameters);
    }

    /// <summary>
    /// Download Report
    /// </summary>
    /// <param name="reportPath">SSRS report path</param>
    /// <param name="reportFormat">Report Format enum</param>
    /// <param name="reportFileName">Report download filename</param>
    /// <param name="parameters">Anonymous object type of report parameters</param>
    /// <remarks>Gets report from SSRS and prompts user to download it</remarks>
    public void DownloadReport(string reportPath, ReportFormat reportFormat, string reportFileName, object parameters)
    {
        // set file name
        reportFileName = !string.IsNullOrWhiteSpace(reportFileName) ? reportFileName : "report";

        // get report url
        var reportUrl = GetReportUrl(reportPath, reportFormat, parameters);

        // get the bytes
        Byte[] reportData = this.WebClient.DownloadData(reportUrl);

        // set content type
        HttpContext.Current.Response.ContentType = reportFormat.GetDescription();

        // add header
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + reportFileName);

        // write binary data
        HttpContext.Current.Response.BinaryWrite(reportData);

        // flush and end
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

    /// <summary>
    /// Get Report Url
    /// </summary>
    /// <param name="reportPath">SSRS report path</param>
    /// <param name="reportFormat">ReportFormat enum</param>
    /// <param name="parameters">Anonymous object type of report parameters</param>
    /// <returns>Url (encoded) to report</returns>
    private string GetReportUrl(string reportPath, ReportFormat reportFormat, object parameters = null)
    {
        /*
         * {0} report base url
         * {1} report path
         * {2} report parameters
         */

        var url = string.Format("{0}?{1}&{2}", this.Credentials.ReportUrl, reportPath, GetParameters(parameters, reportFormat));
        return url;
    }

    /// <summary>
    /// Get Report Path
    /// </summary>
    /// <param name="reportType">SSRS report folder</param>
    /// <param name="reportName">SSRS report name</param>
    /// <returns>SSRS report path</returns>
    private string GetReportPath(string reportType, string reportName)
    {
        return string.Format("/{0}/{1}", reportType, reportName);
    }

    /// <summary>
    /// Get Parameters
    /// </summary>
    /// <param name="parameters">Anonymous object type of report parameters</param>
    /// <param name="reportFormat">ReportFormat enum</param>
    /// <returns>Url (encoded) querystring</returns>
    private string GetParameters(object parameters, ReportFormat reportFormat)
    {
        var routeValues = new RouteValueDictionary(parameters);
        routeValues.Add("rs:Format", reportFormat.ToString());

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        routeValues.ToList().ForEach(x => queryString.Add(x.Key, x.Value.ToString()));
        return queryString.ToString();
    }

}
