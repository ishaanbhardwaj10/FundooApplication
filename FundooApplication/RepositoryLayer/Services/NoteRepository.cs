using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Services
{
    public class NoteRepository : INoteRepository
    {
        DBContext dBContext;
        private readonly IConfiguration config;

        public NoteRepository(DBContext dBContext, IConfiguration config)
        {
            this.dBContext = dBContext;
            this.config = config;
        }

        public NoteEntity AddNotes(NoteModel noteModel, long UserID)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = noteModel.Title;
                noteEntity.Note = noteModel.Note;
                noteEntity.Reminder = noteModel.Reminder;
                noteEntity.Color = noteModel.Color;
                noteEntity.Image = noteModel.Image;
                noteEntity.IsArchive = noteModel.IsArchive;
                noteEntity.IsPin = noteModel.IsPin;
                noteEntity.IsTrash = noteModel.IsTrash;
                noteEntity.CreateAt = noteModel.CreateAt;
                noteEntity.ModifiedAt = noteModel.ModifiedAt;
                noteEntity.UserID = UserID;
                dBContext.NotesTable.Add(noteEntity);
                int result = dBContext.SaveChanges();
                if(result > 0)
                {
                    return noteEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //returns all notes of a particular user
        public List<NoteEntity> GetAllNotes(long userID)
        {
            try
            {
                var result = this.dBContext.NotesTable.Where(e => e.UserID == userID).ToList();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        //only for title and note properties
        public bool UpdateNotes(long noteId, long userId, NoteModel modifyNote)
        {
            try
            {
                var result = this.dBContext.NotesTable.FirstOrDefault(e => e.NoteID == noteId && e.UserID == userId);
                if (result != null)
                {
                    if(modifyNote.Title != null)
                    {
                        result.Title = modifyNote.Title;
                    }
                    if(modifyNote.Note != null)
                    {
                        result.Note = modifyNote.Note;
                    }
                    result.ModifiedAt = DateTime.Now;
                    dBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteNote(long noteId, long userId)
        {
            try
            {
                var result = this.dBContext.NotesTable.FirstOrDefault(e => e.NoteID == noteId && e.UserID == userId);
                if (result != null)
                {
                    this.dBContext.NotesTable.Remove(result);
                    dBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public NoteEntity UpdateColor(long noteId, string color)
        {
            try
            {
                NoteEntity result = this.dBContext.NotesTable.FirstOrDefault(e => e.NoteID == noteId);
                if(result != null)
                {
                    result.Color = color;
                    dBContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string UploadImage(long noteId, long userId, IFormFile img)
        {
            try
            {
                var result = this.dBContext.NotesTable.FirstOrDefault(e => e.NoteID == noteId && e.UserID == userId);

                if (result != null)
                {
                    Account account = new Account(
                        this.config["CloudinarySettings:CloudName"],
                        this.config["CloudinarySettings:ApiKey"],
                        this.config["CloudinarySettings:ApiSecret"]
                        );

                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, img.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = imagePath;
                    this.dBContext.SaveChanges();
                    return "Image uploaded successfully";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool ToggleArchive(long noteId)
        {
            try
            {
                NoteEntity result = this.dBContext.NotesTable.FirstOrDefault(e => e.NoteID == noteId);
                if(result.IsArchive == true)
                {
                    result.IsArchive = false;
                    dBContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.IsArchive = true;
                    dBContext.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public bool TogglePin(long noteId)
        {
            try
            {
                NoteEntity result = this.dBContext.NotesTable.FirstOrDefault(e => e.NoteID == noteId);
                if (result.IsPin == true)
                {
                    result.IsPin = false;
                    dBContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.IsPin = true;
                    dBContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool ToggleTrash(long noteId)
        {
            try
            {
                NoteEntity result = this.dBContext.NotesTable.FirstOrDefault(e => e.NoteID == noteId);
                if (result.IsTrash == true)
                {
                    result.IsTrash = false;
                    dBContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.IsTrash = true;
                    dBContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool RemoveTrashForever(long noteId)
        {
            try
            {
                var result = this.dBContext.NotesTable.FirstOrDefault(e => e.NoteID == noteId);

                if(result.IsTrash == true)
                {
                    this.dBContext.NotesTable.Remove(result);
                    this.dBContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.IsTrash = true;
                    this.dBContext.SaveChanges();
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



    }
}
