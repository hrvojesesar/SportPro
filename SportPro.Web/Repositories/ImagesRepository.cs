using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using SportPro.Web.Interfaces;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SportPro.Web.Repositories;

public class ImagesRepository : IImagesRepository
{
    BlobServiceClient _blobClient;
    BlobContainerClient _containerClient;

    public ImagesRepository()
    {
        var sasToken = "sv=2022-11-02&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2024-05-29T23:12:59Z&st=2024-05-07T15:12:59Z&spr=https&sig=HhFl6BxkbERfR2W30CyB0uJk78lVKp0v8RkUS7sTi1w%3D";
        String blobUri = "https://prosport.blob.core.windows.net/";
        _blobClient = new BlobServiceClient(new Uri($"{blobUri}?{sasToken}"), null);
        _containerClient = _blobClient.GetBlobContainerClient("assets");
    }

    public async Task<IEnumerable<string>> UploadAsync(IEnumerable<IFormFile> files)
    {
        List<string> uploadedImageUrls = new List<string>();

        foreach (var file in files)
        {
            string fileName = file.FileName;
            using (var stream = file.OpenReadStream())
            {
                var blobClient = _containerClient.GetBlobClient(fileName);

                var blobHttpHeaders = new BlobHttpHeaders
                {
                    ContentType = file.ContentType,
                    ContentDisposition = $"inline; filename=\"{fileName}\""
                };

                await blobClient.UploadAsync(stream, blobHttpHeaders);
                uploadedImageUrls.Add(blobClient.Uri.AbsoluteUri);
            }
        }

        return uploadedImageUrls;
    }




    //public async Task<IEnumerable<string>> GetImagesAsync(int id)
    //{
    //    var images = new List<string>();
    //    var searchString = "promo" + id.ToString();
    //    await foreach (var blobItem in _containerClient.GetBlobsAsync())
    //    {
    //        var fileName = blobItem.Name;
    //        var promoId = ExtractPromoId(fileName);
    //        if (promoId == id)
    //        {
    //            var containerName = _containerClient.Name;
    //            var imageUrl = $"https://prosport.blob.core.windows.net/{containerName}/{fileName}";
    //            images.Add(imageUrl);
    //        }
    //    }

    //    return images;
    //}

    //private int ExtractPromoId(string fileName)
    //{
    //    var regex = new Regex(@"promo(\d+)");
    //    var match = regex.Match(fileName);
    //    if (match.Success)
    //    {
    //        return int.Parse(match.Groups[1].Value);
    //    }
    //    return -1;
    //}

    //public async Task<IEnumerable<string>> DeleteImagesAsync(IEnumerable<string> imageUrls)
    //{
    //    var deletedImageUrls = new List<string>();

    //    foreach (var imageUrl in imageUrls)
    //    {
    //        var blobName = imageUrl.Split('/').Last();
    //        var blobClient = _containerClient.GetBlobClient(blobName);
    //        var deleted = await blobClient.DeleteIfExistsAsync();

    //        if (deleted)
    //        {
    //            deletedImageUrls.Add(imageUrl);
    //        }
    //    }

    //    return deletedImageUrls;
    //}
}
