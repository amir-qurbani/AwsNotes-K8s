// Services/S3Service.cs
using Amazon.S3;
using Amazon.S3.Model;

namespace AwsNotes.Services
{
    // Enkel tjänst som laddar upp en fil till S3 och returnerar objekt-nyckeln (key)
    public class S3Service
    {
        private readonly IAmazonS3 _s3;
        private readonly string _bucket;

        public S3Service(IAmazonS3 s3, IConfiguration config)
        {
            _s3 = s3;
            _bucket = config["S3:BucketName"] ?? throw new InvalidOperationException("S3:BucketName saknas i appsettings.json");
        }

        public async Task<string?> UploadAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null; // ingen fil vald

            // Skapa unik key: guid + originalnamn
            var key = $"{Guid.NewGuid():N}_{Path.GetFileName(file.FileName)}";

            using var stream = file.OpenReadStream();

            var request = new PutObjectRequest
            {
                BucketName = _bucket,
                Key = key,
                InputStream = stream,
                ContentType = file.ContentType
            };

            await _s3.PutObjectAsync(request);
            return key; // spara denna i DynamoDB som referens
        }
    }
}
