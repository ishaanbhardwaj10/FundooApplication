using CommonLayer;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteBusiness
    {
        public NoteEntity AddNotes(NoteModel noteModel, long UserID);
        public List<NoteEntity> GetAllNotes(long userID);
        public bool UpdateNotes(long noteId, long userId, NoteModel modifyNote);
        public bool DeleteNote(long noteId, long userId);
        public NoteEntity UpdateColor(long noteId, string color);
        public string UploadImage(long noteId, long userId, IFormFile img);
        public bool ToggleArchive(long noteId);
        public bool TogglePin(long noteId);
        public bool ToggleTrash(long noteId);
        public bool RemoveTrashForever(long noteId);

    }
}
