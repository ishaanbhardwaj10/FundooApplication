using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        ILabelBusiness labelBusiness;
        DBContext dbContext;

        public LabelController(ILabelBusiness labelBusiness, DBContext dBContext)
        {
            this.labelBusiness = labelBusiness;
            dbContext = dBContext;
        }

        [HttpPost]
        [Route("AddLabel")]
        public IActionResult AddLabel(string labelName, long userId, long noteId)
        {
            try
            {
                var result = this.labelBusiness.AddLabel(labelName, userId, noteId);
                if(result)
                {
                    return this.Ok(new { success = true, message = "Label successfully added", data = result});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to add label" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllLabels")]
        public IActionResult GetAllLabels(long labelId)
        {
            try
            {
                var result = this.labelBusiness.GetAllLabels(labelId);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "Labels fetched successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "fetch operation failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpDelete]
        [Route("RemoveLabel")]
        public IActionResult RemoveLabel(long labelId)
        {
            try
            {
                var result = this.labelBusiness.RemoveLabel(labelId);
                if(result)
                {
                    return this.Ok(new { success = true, message = "Label removed successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to remove label" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpPut]
        [Route("UpdateLabel")]
        public IActionResult UpdateLabel(string newLabelName, long labelId)
        {
            try
            {
                var result = this.labelBusiness.UpdateLabel(newLabelName, labelId);
                if (result)
                {
                    return this.Ok(new { success = true, message = "Label updated successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to update label" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
