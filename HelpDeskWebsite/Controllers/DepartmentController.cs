using System;
using System.Collections.Generic;
using System.Web.Http;
using HelpDeskViewModel;

namespace HelpDeskWebsite.Controllers
{
    public class DepartmentController : ApiController
    {
        [Route("api/departments")]
        public IHttpActionResult GetAll()
        {
            try
            {
                DepartmentViewModel dep = new DepartmentViewModel();
                List<DepartmentViewModel> allDepartments = dep.GetAll();
                return Ok(allDepartments);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
    }
}
