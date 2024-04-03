using Infrastructure.Abstraction;
using Message.Models;

namespace Message.Abstraction;

public interface IMessageRepository : ICrudRepository<Message>
{
    Task<List<Message>> GetPagedAsync(int page, int itemsPerPage);

    public Task<List<ChatModel>> GetAllChatsByUserIdAsync(Guid userId);

    public Task<List<Message>> GetMessagesByChatId(string chatId);
}