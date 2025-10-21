using MongoDB.Driver;
using AwsNotes.Models;

namespace AwsNotes.Data
{
    public class MongoDbService
    {
        private readonly IMongoCollection<NoteItem> _notesCollection;

        public MongoDbService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb")
                                   ?? "mongodb://localhost:27017";

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("AwsNotesDb");
            _notesCollection = database.GetCollection<NoteItem>("Notes");
        }

        public async Task<List<NoteItem>> GetNotesAsync()
        {
            return await _notesCollection.Find(_ => true).ToListAsync();
        }

        public async Task AddNoteAsync(NoteItem note)
        {
            await _notesCollection.InsertOneAsync(note);
        }
    }
}
