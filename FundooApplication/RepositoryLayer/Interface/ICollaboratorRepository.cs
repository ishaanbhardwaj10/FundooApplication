using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorRepository
    {
        public CollaboratorEntity AddCollaborator(string email, long noteId);
        public bool RemoveCollaborator(long collabId);
        public List<CollaboratorEntity> GetAllCollaboratorsByUserId(long userId);
        public List<CollaboratorEntity> GetAllCollaboratorsByNoteId(long noteId);

    }
}
