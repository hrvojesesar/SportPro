namespace SportPro.Web.Interfaces;

public interface IImagesRepository
{
    Task<IEnumerable<string>> UploadAsync(IEnumerable<IFormFile> files);
//    Task<IEnumerable<string>> GetImagesAsync(int id);
//    Task<IEnumerable<string>> DeleteImagesAsync(IEnumerable<string> imageUrls);
}