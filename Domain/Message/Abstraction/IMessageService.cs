using Infrastructure;
using Infrastructure.Abstraction;
using Message.Dto;
using Message.Models;

namespace Message.Abstraction;

public interface IMessageService : ICrudService<Message, CreatingMessageDto, UpdatingMessageDto, MessageDto>
{
    public Task<List<ChatModel>> GetChatsByUserIdAsync(Guid id);
    public IQueryable<Message> GetAll(bool noTracking = false);
}