ReportService
=============

Simple SSRS Report Download Service
Usage:
        public ActionResult Index()
        {
            var report = new ReportService(
                "/Centerline/CenterLineByCounty",
                ReportFormat.PDF,
                "2012-CenterLineByCounty",
                new { Year = 2012, GeoExtent = "County" });

            return View();
        }
