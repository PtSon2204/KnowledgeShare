namespace KnowledgeShare.API.Repositories.Interface
{
    public interface ILoginRepository
    {
        Task<User> LoginAsync(string email, string password);
    }
}
