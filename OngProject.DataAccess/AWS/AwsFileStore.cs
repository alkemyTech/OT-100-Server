using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OngProject.Application.Interfaces;

namespace OngProject.DataAccess.AWS
{
    public class AwsFileStore : IFileStore
    {
        private readonly AwsManageCredentials _aws;
        public AwsFileStore(IOptionsMonitor<AwsManageCredentials> options)
        {
            _aws = options.CurrentValue;
        }
        
        public async Task<string> SaveFile(IFormFile image)
        {
            var credentials = new BasicAWSCredentials(_aws.AccessKeyId, _aws.SecretAccessKey);

            using (var client = new AmazonS3Client(credentials, _aws.Region))
            {
                await using (MemoryStream ms = new MemoryStream())
                {
                    await image.CopyToAsync(ms);

                    var putRequest = new TransferUtilityUploadRequest()
                    {
                        InputStream = ms,
                        Key = image.FileName,
                        BucketName = _aws.Bucket,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var result = new TransferUtility(client);
                    await result.UploadAsync(putRequest);
                }
            }
            var url = $"https://ot100-bucket.s3.amazonaws.com/{image.FileName}";
            
            return url;
        }
        
        public async Task<string> EditFile(IFormFile image, string url)
        {
            await DeleteFile(url);
            return await SaveFile(image);
        }
        
        public async Task DeleteFile(string url)
        {
            string[] result = url.Split("/");
            var fileName = result.Last();

            var credentials = new BasicAWSCredentials(_aws.AccessKeyId, _aws.SecretAccessKey);

            using (var client = new AmazonS3Client(credentials, _aws.Region)){
                var request = new DeleteObjectRequest()
                {
                    BucketName = _aws.Bucket,
                    Key = fileName,
                };

                var response = await client.DeleteObjectAsync(request);
            }  
        }

    }
}