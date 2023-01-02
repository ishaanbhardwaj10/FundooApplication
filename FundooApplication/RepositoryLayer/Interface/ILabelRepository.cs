using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepository
    {
        public bool AddLabel(string labelName, long userId, long noteId);
        public List<LabelEntity> GetAllLabels(long labelId);
        public bool RemoveLabel(long labelId);
        public bool UpdateLabel(string newLabelName, long labelId);

    }
}
