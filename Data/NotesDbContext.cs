using Microsoft.EntityFrameworkCore;
using AwsNotes.Models;

namespace AwsNotes.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options)
            : base(options)
        {
        }

        public DbSet<NoteItem> Notes { get; set; }
    }
}
