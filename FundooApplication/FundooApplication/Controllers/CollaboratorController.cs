using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        ICollaboratorBusiness collaboratorBusiness;

        public CollaboratorController(ICollaboratorBusiness collaboratorBusiness)
        {
            this.collaboratorBusiness = collaboratorBusiness;
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult AddCollaborator(string email, long noteId)
        {
            try
            {
                var result = this.collaboratorBusiness.AddCollaborator(email, noteId);

                if(result != null)
                {
                    return this.Ok(new { success = true, message = "collaborator added successfully", data = result});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to add collaborator" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpDelete]
        [Route("Delete")]
        public IActionResult RemoveCollaborator(long collabId)
        {
            try
            {
                var result = this.collaboratorBusiness.RemoveCollaborator(collabId);

                if(result)
                {
                    return this.Ok(new { success = true, message = "collaborator removed successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed to remove collaborator" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllByUserId")]
        public IActionResult GetAllCollaboratorsByUserId(long userId)
        {
            try
            {
                var result = this.collaboratorBusiness.GetAllCollaboratorsByUserId(userId);

                if(result != null)
                {
                    return this.Ok(new { success = true, message = "collaborators fetched successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "failed to fetch collaborator"
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [Route("GetAllByNoteId")]
        public IActionResult GetAllCollaboratorByNoteId(long noteId)
        {
            try
            {
                var result = this.collaboratorBusiness.GetAllCollaboratorsByNoteId(noteId);

                if (result != null)
                {
                    return this.Ok(new { success = true, message = "collaborators fetched successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "failed to fetch collaborator"
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
