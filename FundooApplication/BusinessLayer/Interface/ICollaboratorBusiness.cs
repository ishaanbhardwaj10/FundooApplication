using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorBusiness
    {
        public CollaboratorEntity AddCollaborator(string email, long noteId);
        public bool RemoveCollaborator(long collabId);
        public List<CollaboratorEntity> GetAllCollaboratorsByUserId(long userId);
        public List<CollaboratorEntity> GetAllCollaboratorsByNoteId(long noteId);

    }
}
