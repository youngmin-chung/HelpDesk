using System;
using System.Collections.Generic;
using System.Web.Http;
using HelpDeskViewModel;

namespace HelpDeskWebsite.Controllers
{
    public class CallController : ApiController
    {
        [Route("api/calls/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                CallViewModel cal = new CallViewModel();
                cal.Id = id;
                cal.GetById();
                return Ok(cal);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }//end Get()

        [Route("api/calls")]
        public IHttpActionResult Put(CallViewModel cal)
        {
            try
            {
                int retVal = cal.Update();

                switch (retVal)
                {
                    case 1:
                        return Ok("Call " + cal.Id + " updated!");
                    case -1:
                        return Ok("Call " + cal.Id + " not updated!");
                    case -2:
                        return Ok("Data is stale for " + cal.Id + ", Call not updated!");
                    default:
                        return Ok("Call " + cal.Id + " not updated!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        } //end Put()

        [Route("api/calls")]
        public IHttpActionResult GetAll()
        {
            try
            {
                CallViewModel cal = new CallViewModel();
                List<CallViewModel> allCalls = cal.GetAll();
                return Ok(allCalls);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        } //end GetAll()

        [Route("api/calls")]
        public IHttpActionResult Post(CallViewModel cal)
        {
            try
            {
                cal.Add();
                if (cal.Id > 0)
                {
                    return Ok("Call " + cal.Id + " added!");
                }
                else
                {
                    return Ok("Call " + cal.Id + " not added!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Creation failed - Contact Tech Support");
            }
        } //end Post()

        [Route("api/calls/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                CallViewModel cal = new CallViewModel();
                cal.Id = id;

                if (cal.Delete() == 1)
                {
                    return Ok("Call deleted!");
                }
                else
                {
                    return Ok("Call not deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - Contact Tech Support");
            }
        } //end Delete()
    } //end class
}
