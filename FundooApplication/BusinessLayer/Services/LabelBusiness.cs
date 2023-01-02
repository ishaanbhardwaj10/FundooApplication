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
    public class LabelBusiness : ILabelBusiness
    {

        private readonly ILabelRepository labelRepository;

        public LabelBusiness(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }

        public bool AddLabel(string labelName, long userId, long noteId)
        {
            try
            {
                return labelRepository.AddLabel(labelName, userId, noteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public List<LabelEntity> GetAllLabels(long labelId)
        {
            try
            {
                return labelRepository.GetAllLabels(labelId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveLabel(long labelId)
        {
            try
            {
                return labelRepository.RemoveLabel(labelId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateLabel(string newLabelName, long labelId)
        {
            try
            {
                return labelRepository.UpdateLabel(newLabelName, labelId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
