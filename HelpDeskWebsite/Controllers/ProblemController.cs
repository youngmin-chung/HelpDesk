using System;
using System.Collections.Generic;
using System.Web.Http;
using HelpDeskViewModel;

namespace HelpDeskWebsite.Controllers
{
    public class ProblemController : ApiController
    { //GET api/<controller>
        [Route("api/problems")]
        public IHttpActionResult Get()
        {
            try
            {
                ProblemViewModel emp = new ProblemViewModel();
                List<ProblemViewModel> allProblems = emp.GetAll();
                return Ok(allProblems);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }//end route

        //get by id
        [Route("api/problems/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                ProblemViewModel prob = new ProblemViewModel();
                prob.Id = id;
                prob.GetById(); //emp.GetByID
                return Ok(prob);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }//end route

        [Route("api/problems")]
        public IHttpActionResult Put(ProblemViewModel prob)
        {
            try
            {
                int retVal = prob.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Problem " + prob.Description + " updated!");
                    case -1:
                        return Ok("Problem " + prob.Description + " not updated!");
                    case -2:
                        return Ok("Data is not stale for " + prob.Description + ", Problem not updated!");
                    default:
                        return Ok("Problem " + prob.Description + " not updated!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }//end route

        //delete
        [Route("api/problems")]
        public IHttpActionResult Delete(ProblemViewModel prob)
        {
            try
            {
                int retVal = prob.Delete();

                switch (retVal)
                {
                    case 1:
                        return Ok("Problem " + prob.Description + " deleted!");

                    default:
                        return Ok("Problem " + prob.Description + " not deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }//end route

        [Route("api/problems")]
        public IHttpActionResult Post(ProblemViewModel prob)
        {
            try
            {
                prob.Add();
                return Ok("Problem " + prob.Description + " created!");

            }
            catch (Exception ex)
            {
                return BadRequest("Create failed - " + ex.Message);
            }
        }//end route
    }//end class
}
