namespace SportPro.Web.Interfaces;

public interface IImagesRepository
{
    Task<IEnumerable<string>> UploadAsync(IEnumerable<IFormFile> files);
}