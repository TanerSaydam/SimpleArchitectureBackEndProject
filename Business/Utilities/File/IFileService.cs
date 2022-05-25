using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IFileService
    {
        string FileSaveToServer(IFormFile file, string filePath);
        string FileSaveToFtp(IFormFile file);
        byte[] FileConvertByteArrayToDatabase(IFormFile file);
    }
}
