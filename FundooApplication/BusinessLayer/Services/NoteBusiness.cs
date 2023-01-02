using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBusiness : INoteBusiness
    {
        private readonly INoteRepository noteRepository;

        public NoteBusiness(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public NoteEntity AddNotes(NoteModel noteModel, long UserID)
        {
            try
            {
                return noteRepository.AddNotes(noteModel, UserID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public List<NoteEntity> GetAllNotes(long userID)
        {
            try
            {
                return noteRepository.GetAllNotes(userID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public bool UpdateNotes(long noteId, long userId, NoteModel modifyNote)
        {
            try
            {
                return noteRepository.UpdateNotes(noteId, userId, modifyNote);
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
                return this.noteRepository.DeleteNote(noteId, userId);
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
                return this.noteRepository.UpdateColor(noteId, color);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public string UploadImage(long noteId, long userId, IFormFile img)
        {
            try
            {
                return this.noteRepository.UploadImage(noteId, userId, img);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public bool ToggleArchive(long noteId)
        {
            try
            {
                return this.noteRepository.ToggleArchive(noteId);
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
                return this.noteRepository.TogglePin(noteId);
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
                return this.noteRepository.ToggleTrash(noteId);
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
                return this.noteRepository.RemoveTrashForever(noteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    }
}
