using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AwsNotes.Models;

namespace AwsNotes.Services
{
    public class NotesRepository
    {
        private readonly IDynamoDBContext _db;

        public NotesRepository(IAmazonDynamoDB dynamoDb)
        {
            _db = new DynamoDBContext(dynamoDb);
        }

        public async Task<List<NoteItem>> GetAllAsync()
        {
            var conditions = new List<ScanCondition>();
            return (await _db.ScanAsync<NoteItem>(conditions).GetRemainingAsync()).OrderByDescending(n => n.CreatedUtc).ToList();
        }

        public async Task<NoteItem?> GetByIdAsync(string id)
        {
            return await _db.LoadAsync<NoteItem>(id);
        }

        public async Task SaveAsync(NoteItem item)
        {
            await _db.SaveAsync(item);
        }

        public async Task DeleteAsync(string id)
        {
            await _db.DeleteAsync<NoteItem>(id);
        }
    }

}