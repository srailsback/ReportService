ReportService
=============
<p>Simple SSRS Report Download Service. The service does one thing; downloads SSRS reports.</p>
<h3>Setup</h3>
<p>Add appSetting keys to Web.Config and add clasess to project.</p>
<pre>
&lt;add key="ReportServerUrl" value="http://server/reportserver"/&gt;
&lt;add key="ReportServerUsername" value=""/&gt;
&lt;add key="ReportServerPassword" value=""/&gt;
</pre>


<h3>Get Report by SSRS Path</h3>
<pre>
public ActionResult Index()
{
        var report = new ReportService(
                "/Centerline/CenterLineByCounty",
                ReportFormat.PDF,
                "2012-CenterLineByCounty",
                new { Year = 2012, GeoExtent = "County" });

        return View();
}
</pre>

<h3>Get Report by SSRS Folder and Report Name</h3>
<pre>
public ActionResult Index()
{
        var report = new ReportService(
                "Centerline", "CenterLineByCounty",
                ReportFormat.PDF,
                "2012-CenterLineByCounty",
                new { Year = 2012, GeoExtent = "County" });

        return View();
}
</pre>
