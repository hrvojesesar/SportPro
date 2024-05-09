using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using SportPro.Web.Interfaces;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SportPro.Web.Repositories;

public class ImagesRepository : IImagesRepository
{
    private readonly BlobServiceClient _blobClient;
    private readonly BlobContainerClient _containerClient;

    public ImagesRepository(IConfiguration configuration)
    {
        var azureConfig = configuration.GetSection("Azure");

        var sasToken = azureConfig["SasToken"];
        var blobUri = azureConfig["Storage"];
        var containerName = azureConfig["ContainerName"];

        _blobClient = new BlobServiceClient(new Uri($"{blobUri}?{sasToken}"), null);
        _containerClient = _blobClient.GetBlobContainerClient(containerName);
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
}