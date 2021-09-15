using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpDeskWebsite.Reports;

namespace HelpDeskWebsite.Controllers
{
    public class PDFController : ApiController
    {
        

        [Route("api/employeereport")]
        public IHttpActionResult GetEmployeeReport()
        {
            try
            {
                EmployeeReport report = new EmployeeReport();
                report.doIt();
                return Ok("report generated");
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - Coundn't generate sample report");
            }
        }

        [Route("api/callreport")]
        public IHttpActionResult GetCallReport()
        {
            try
            {
                CallReport report = new CallReport();
                report.doIt();
                return Ok("report generated");
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - Coundn't generate sample report");
            }
        }
    }
}
