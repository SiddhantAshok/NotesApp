using Microsoft.EntityFrameworkCore;
using Notes.API.Models.Entities;

namespace Notes.API.Data
{
    public class NotesDBContext : DbContext
    {
        public NotesDBContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<Note> Notes { get; set; }
    }
}
