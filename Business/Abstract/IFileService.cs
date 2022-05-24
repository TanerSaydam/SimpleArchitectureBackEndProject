using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IFileService
    {
        string FileSave(IFormFile file, string filePath);
    }
}
