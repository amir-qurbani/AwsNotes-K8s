using Microsoft.AspNetCore.Http;

namespace AwsNotes.Models
{
    public class NoteCreateViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public IFormFile? File { get; set; }
    }
}