

namespace todoListApi.Services
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
