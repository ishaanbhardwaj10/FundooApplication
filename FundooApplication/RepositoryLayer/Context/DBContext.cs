
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Context
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions options) : base(options)
        { }
        public DbSet<UserEntity> userTable { get; set; }

        public DbSet<NoteEntity> NotesTable { get; set; }

        public DbSet<LabelEntity> LabelTable { get; set; }


    }
}
