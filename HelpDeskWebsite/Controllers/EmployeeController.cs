using System;
using System.Collections.Generic;
using System.Web.Http;
using HelpDeskViewModel;

namespace HelpDeskWebsite.Controllers
{

    public class EmployeeController : ApiController
    {
        [Route("api/Employees/{name}")]
        public IHttpActionResult Get(string name)
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.Lastname = name;
                emp.GetByLastname();
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/Employees")]
        public IHttpActionResult GetAll()
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                List<EmployeeViewModel> allEmployees = emp.GetAll();
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        //GET api/<controller>
        [Route("api/technicians")]
        public IHttpActionResult GetTech()
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                List<EmployeeViewModel> allEmployees = emp.GetTech();
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }//end get tech route

        [Route("api/Employees")]
        public IHttpActionResult Put(EmployeeViewModel emp)
        {
            try
            {
                int retVal = emp.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Employee " + emp.Lastname + " updated!");
                    case -1:
                        return Ok("Employee " + emp.Lastname + " not updated!");
                    case -2:
                        return Ok("Data is stale for " + emp.Lastname + ", Employee not updated!");
                    default:
                        return Ok("Employee " + emp.Lastname + " not updated!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/Employees")]
        public IHttpActionResult Post(EmployeeViewModel emp)
        {
            try
            {
                emp.Add();
                if (emp.Id > 0)
                {
                    return Ok("Employee " + emp.Lastname + " added!");
                }
                else
                {
                    return Ok("Employee " + emp.Lastname + " not added!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Creation failed - Contact Tech Support");
            }
        }

        [Route("api/Employees/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.Id = id;

                if (emp.Delete() == 1)
                {
                    return Ok("Employee deleted!");
                }
                else
                {
                    return Ok("Employee not deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Delete failed - Contact Tech Support");
            }
        }
    }
}
