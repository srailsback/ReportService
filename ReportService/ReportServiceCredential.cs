using System.Net;

/// <summary>
/// Report Service Credential
/// </summary>
/// <remarks>
/// Creates user network credentials and web client so we may connect into SSRS and pull out reports
/// Relies on appSettings defined in web.config
/// - ReportServerUrl, the SSRS reporting server
/// - ReportServerUsername, the user that will connect
/// - ReportServerPassword, the user's password
/// Note -- consider rolling a unique user rather that rely on a persons credentials
/// </remarks>
public class ReportServiceCredential
{
    public string ReportUrl { get; set; }
    private string ReportUser { get; set; }
    private string ReportPassword { get; set; }
    private string ReportDomain { get; set; }


    public ReportServiceCredential()
    {
        this.ReportUrl = ConfigHelper.GetAppSetting("ReportServerUrl");
        this.ReportUser = ConfigHelper.GetAppSetting("ReportServerUsername");
        this.ReportPassword = ConfigHelper.GetAppSetting("ReportServerPassword");
    }

    public ReportServiceCredential(string userName, string password)
    {
        this.ReportUrl = ConfigHelper.GetAppSetting("ReportServerUrl");
        this.ReportUser = userName;
        this.ReportPassword = password;
    }

    public ReportServiceCredential(string userName, string password, string reportUrl)
    {
        this.ReportUrl = reportUrl;
        this.ReportUser = userName;
        this.ReportPassword = password;
    }

    public ReportServiceCredential(string reportUrl)
    {
        this.ReportUrl = reportUrl;
        this.ReportUser = ConfigHelper.GetAppSetting("ReportServerUsername");
        this.ReportPassword = ConfigHelper.GetAppSetting("ReportServerPassword");
    }

    public WebClient GetClient()
    {
        var networkCredential = new NetworkCredential(this.ReportUser, this.ReportPassword);
        var client = new WebClient();
        client.Credentials = networkCredential;
        return client;
    }
}
