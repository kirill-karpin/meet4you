using Infrastructure.Abstraction;

namespace Message.Abstraction;

public interface IMessageRepository : ICrudRepository<Message>
{
    Task<List<Message>> GetPagedAsync(int page, int itemsPerPage);

    public Task<List<Message>> GetAllChatsByUserIdAsync(Guid userId);
}