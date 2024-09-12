using System;
using System.Diagnostics;
using ApplicationCore.Entities;
using Infrastructure.Reports.NoSQL;
using Microsoft.AspNetCore.Mvc;
using MySqlReport = Infrastructure.Reports.MySQL;
using NoSqlReport = Infrastructure.Reports.NoSQL;

namespace WebApp.Controllers
{
	public class ReportController: Controller
	{
        private readonly MySqlReport.ReportGenerator _mySqlReportGenerator;
        private readonly NoSqlReport.ReportGenerator _noSqlReportGenerator;
        private readonly ILogger<HomeController> _logger;

        public ReportController(MySqlReport.ReportGenerator mySqlReportGenerator,
			NoSqlReport.ReportGenerator noSqlReportGenerator, ILogger<HomeController> logger)
		{
            _mySqlReportGenerator = mySqlReportGenerator;
            _noSqlReportGenerator = noSqlReportGenerator;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var report = new List<ReportRow>();

            if (GlobalValues.CurrentDBMS == DBMS.MySQL)
            {
                report.AddRange(_mySqlReportGenerator.Generate());
            }
            else if (GlobalValues.CurrentDBMS == DBMS.MongoDB)
            {
                report.AddRange(_noSqlReportGenerator.Generate());
            }
            return View(report);
        }
	}
}

