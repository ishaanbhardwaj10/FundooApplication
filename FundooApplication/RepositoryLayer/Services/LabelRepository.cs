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
    public class LabelRepository : ILabelRepository
    {
        DBContext dBContext;

        public LabelRepository(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public bool AddLabel(string labelName, long userId, long noteId)
        {
            try
            {
                var result = this.dBContext.NotesTable.FirstOrDefault(e => e.NoteID == noteId);
                if (result != null)
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.LabelName = labelName;
                    labelEntity.UserID = userId;
                    labelEntity.NoteID = noteId;
                    this.dBContext.LabelTable.Add(labelEntity);
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

        public List<LabelEntity> GetAllLabels(long labelId)
        {
            try
            {
                var result = this.dBContext.LabelTable.Where(e => e.LabelID == labelId).ToList();
                return result;
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
                var result = this.dBContext.LabelTable.FirstOrDefault(e => e.LabelID == labelId);
                if (result != null)
                {
                    this.dBContext.LabelTable.Remove(result);
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


        public bool UpdateLabel(string newLabelName, long labelId)
        {
            try
            {
                var result = this.dBContext.LabelTable.FirstOrDefault(e => e.LabelID == labelId);
                if(result != null)
                {
                    result.LabelName = newLabelName;
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



    }
}
