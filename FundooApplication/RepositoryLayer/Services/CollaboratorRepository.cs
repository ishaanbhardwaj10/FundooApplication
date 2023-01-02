using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        DBContext dBContext;

        public CollaboratorRepository(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public CollaboratorEntity AddCollaborator(string email, long noteId)
        {
            try
            {
                var emailResult = this.dBContext.userTable.FirstOrDefault(e => e.EmailID == email);
                var noteIdResult = this.dBContext.NotesTable.FirstOrDefault(e => e.NoteID == noteId);

                if(emailResult != null && noteIdResult != null)
                {
                    CollaboratorEntity collaboratorEntity = new CollaboratorEntity();
                    collaboratorEntity.CollaboratorEmail = emailResult.EmailID;
                    collaboratorEntity.UserID = emailResult.UserID;
                    collaboratorEntity.NoteID = noteIdResult.NoteID;
                    this.dBContext.CollaboratorTable.Add(collaboratorEntity);
                    this.dBContext.SaveChanges();
                    return collaboratorEntity;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public bool RemoveCollaborator(long collabId)
        {
            try
            {
                var result = this.dBContext.CollaboratorTable.FirstOrDefault(e => e.CollaboratorID == collabId);

                if(result != null)
                {
                    this.dBContext.CollaboratorTable.Remove(result);
                    this.dBContext.SaveChanges();
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


        public List<CollaboratorEntity> GetAllCollaboratorsByUserId(long userId)
        {
            try
            {
                var result = this.dBContext.CollaboratorTable.Where(e => e.UserID == userId).ToList();

                if(result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public List<CollaboratorEntity> GetAllCollaboratorsByNoteId(long noteId)
        {
            try
            {
                var result = this.dBContext.CollaboratorTable.Where(e => e.NoteID == noteId).ToList();

                if (result != null)
                {
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


    }
}
