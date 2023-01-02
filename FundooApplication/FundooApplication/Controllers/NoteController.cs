using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System.Text;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private INoteBusiness noteBusiness;
        private DBContext dBContext;
        private readonly ILogger<NoteController> _logger;
        //private readonly IMemoryCache memoryCache;
        //private readonly IDistributedCache distributedCache;

        public NoteController(INoteBusiness noteBusiness, DBContext dBContext, ILogger<NoteController> logger)//, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.noteBusiness = noteBusiness;
            this.dBContext = dBContext;
            this._logger = logger;
            _logger.LogDebug(1, "NLog injected into NoteController");
            //this.memoryCache = memoryCache;
            //this.distributedCache = distributedCache;
        }

        [HttpPost("AddNotes")]
        public IActionResult AddNotes(NoteModel note)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                var result = this.noteBusiness.AddNotes(note, userid);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note added successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note add operation failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Route("GetAllNotes")]
        public IActionResult GetAllNotes(long userId)
        {
            try
            {
                var result = this.noteBusiness.GetAllNotes(userId);
                _logger.LogInformation("Hello, this is get all notes!");
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "notes fetched successfully", data = result});
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


        [HttpPut]
        [Route("UpdateNotes")]
        public IActionResult UpdateNotes(long noteId, long userId, NoteModel modifyNote)
        {
            try
            {
                var result = this.noteBusiness.UpdateNotes(noteId, userId, modifyNote);
                if(result)
                {
                    return this.Ok(new { success = true, message = "note updated successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "note update failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpDelete]
        [Route("DeleteNote")]
        public IActionResult DeleteNote(long noteId, long userId)
        {
            try
            {
                var result = this.noteBusiness.DeleteNote(noteId, userId);
                if(result)
                {
                    return this.Ok(new { success = true, message = "Note deleted successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note deletion failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpPut]
        [Route("UpdateColor")]
        public IActionResult UpdateColor(long noteId, string color)
        {
            try
            {
                var result = this.noteBusiness.UpdateColor(noteId, color);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "Color updated successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "update operation failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UploadImage")]
        public IActionResult UploadImage(long noteId, long userId, IFormFile img)
        {
            try
            {
                var result = this.noteBusiness.UploadImage(noteId, userId, img);

                if(result == "Image uploaded successfully")
                {
                    return this.Ok(new { success = true, message = "Image updated successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "upload operation failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpPut]
        [Route("IsArchive")]
        public IActionResult ToggleArchive(long noteId)
        {
            try
            {
                var result = this.noteBusiness.ToggleArchive(noteId);
                if(result != null)
                {
                    if(result)
                    {
                        return this.Ok(new { success = true, message = "Archive toggled on", data = result });
                    }
                    else
                    {
                        return this.Ok(new { success = true, message = "Archive toggled off", data = result });
                    }
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Archive toggling failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpPut]
        [Route("IsPin")]
        public IActionResult TogglePin(long noteId)
        {
            try
            {
                var result = this.noteBusiness.TogglePin(noteId);
                if (result != null)
                {
                    if (result)
                    {
                        return this.Ok(new { success = true, message = "Pin toggled on", data = result });
                    }
                    else
                    {
                        return this.Ok(new { success = true, message = "Pin toggled off", data = result });
                    }
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Pin toggling failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("IsTrash")]
        public IActionResult ToggleTrash(long noteId)
        {
            try
            {
                var result = this.noteBusiness.ToggleTrash(noteId);
                if (result != null)
                {
                    if (result)
                    {
                        return this.Ok(new { success = true, message = "Trash toggled on", data = result });
                    }
                    else
                    {
                        return this.Ok(new { success = true, message = "Trash toggled off", data = result });
                    }
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Trash toggling failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("RemoveTrashForever")]
        public IActionResult RemoveTrashForever(long noteId)
        {
            try
            {
                var result = this.noteBusiness.RemoveTrashForever(noteId);

                if(result)
                {
                    return this.Ok(new { success = true, message = "Trash removed successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "remove operation failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //[HttpGet]
        //[Route("Redis")]
        //public async Task<IActionResult> GetAllNotesUsingRedisCache()
        //{
        //    var cacheKey = "noteList";
        //    string serializedNoteList;
        //    var noteList = new List<NoteEntity>();
        //    var redisNoteList = await distributedCache.GetAsync(cacheKey);
        //    if (redisNoteList != null)
        //    {
        //        serializedNoteList = Encoding.UTF8.GetString(redisNoteList);
        //        noteList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedNoteList);
        //    }
        //    else
        //    {
        //        noteList = dBContext.NotesTable.ToList();
        //        serializedNoteList = JsonConvert.SerializeObject(noteList);
        //        redisNoteList = Encoding.UTF8.GetBytes(serializedNoteList);
        //        var options = new DistributedCacheEntryOptions()
        //        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
        //            .SetSlidingExpiration(TimeSpan.FromMinutes(2));
        //        await distributedCache.SetAsync(cacheKey, redisNoteList, options);
        //    }
        //    return Ok(noteList);
        //}




    }
}
