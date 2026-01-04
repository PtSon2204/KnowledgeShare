namespace KnowledgeShare.API.Repositories.Interface
{
    public interface ILoginService
    {
        Task<string> LoginAsync(string email, string password);
    }
}
