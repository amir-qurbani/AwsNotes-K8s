using System.Runtime.CompilerServices;
using Amazon.DynamoDBv2.DataModel;

namespace AwsNotes.Models
{
    [DynamoDBTable("Notes")]    
    public class NoteItem
    {
        [DynamoDBHashKey]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Title { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string? S3Key { get; set; } // kan vara null om ingen fil laddats upp
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}