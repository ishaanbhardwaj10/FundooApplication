using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelID { get; set; }
        public string LabelName { get; set; }



        [ForeignKey("users")]
        public long UserID { get; set; }
        public virtual UserEntity users { get; set; }


        [ForeignKey("notes")]
        public long NoteID { get; set; }
        public virtual NoteEntity notes { get; set; }

    }
}
