namespace KnowledgeShare.API.Services.Interface
{
    //class này hỗ trợ upload file
    public interface IStorageService
    {
        string GetFileUrl(string fileName);
        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
        Task DeleteFileAsync(string fileName);
    }
}
