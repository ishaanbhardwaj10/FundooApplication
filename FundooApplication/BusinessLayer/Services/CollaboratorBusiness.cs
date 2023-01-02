using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollaboratorBusiness : ICollaboratorBusiness
    {
        private readonly ICollaboratorRepository collaboratorRepository;

        public CollaboratorBusiness(ICollaboratorRepository collaboratorRepository)
        {
            this.collaboratorRepository = collaboratorRepository;
        }

        public CollaboratorEntity AddCollaborator(string email, long noteId)
        {
            try
            {
                return collaboratorRepository.AddCollaborator(email, noteId);
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
                return collaboratorRepository.RemoveCollaborator(collabId);
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
                return collaboratorRepository.GetAllCollaboratorsByUserId(userId);
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
                return collaboratorRepository.GetAllCollaboratorsByNoteId(noteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
