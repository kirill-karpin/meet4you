using Infrastructure;
using Infrastructure.Abstraction;
using Message.Dto;

namespace Message.Abstraction;

public interface IMessageService : ICrudService<Message, CreatingMessageDto, UpdatingMessageDto, MessageDto>
{
    public Task<List<Message>> GetChatsByUserIdAsync(Guid id);
    public IQueryable<Message> GetAll(bool noTracking = false);
}