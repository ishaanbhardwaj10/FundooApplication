using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class CollaboratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollaboratorID { get; set; }
        public string CollaboratorEmail { get; set; }


        [ForeignKey("userCollab")]
        public long UserID { get; set; }
        public virtual UserEntity userCollab { get; set; }


        [ForeignKey("noteCollab")]
        public long NoteID { get; set; }
        public virtual NoteEntity noteCollab { get; set; }

    }
}
