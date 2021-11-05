using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly AmazonS3Client s3Client;
        private const string BUCKET_NAME = "tradebucked";
        private const string FOLDER_NAME = "images";
        private const double DURATION = 24;
        public ImageService()
        {
            s3Client = new AmazonS3Client("AKIAWE7ZRFHC4KZUHFKZ","QBn2shoF7HTvllqXJLvrIR0xpkezQChpzck2Lp4l", RegionEndpoint.EUWest1);
        }
        public async Task<string> UploadImageToBucket(string email, IFormFile image)
        {
            if(image == null) {return null;}
            string fileName = image.FileName;
            string objectKey = $"{FOLDER_NAME}/{email}/{fileName}";

            using (Stream fileToUpload = image.OpenReadStream()) 
            {
                var putObjectRequest = new PutObjectRequest();
                putObjectRequest.BucketName = BUCKET_NAME;
                putObjectRequest.Key = objectKey;
                putObjectRequest.InputStream = fileToUpload;
                putObjectRequest.ContentType = image.ContentType;

                var response = await s3Client.PutObjectAsync(putObjectRequest);
                
                return GeneratePreSignedURL(objectKey);
            }
        }


        private string GeneratePreSignedURL(string objectKey)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = BUCKET_NAME,
                Key = objectKey,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddHours(DURATION)
            };

            return s3Client.GetPreSignedURL(request);

        }
    }
}