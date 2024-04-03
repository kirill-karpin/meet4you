using Message.Abstraction;
using AutoMapper;
using Infrastructure;
using Infrastructure.Abstraction;
using Message.Dto;
using Message.Models;

namespace Message;

public class MessageService : CrudService<Message, CreatingMessageDto, UpdatingMessageDto, MessageDto>, IMessageService
{
    private readonly IMapper _mapper;
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMapper mapper,
        IMessageRepository messageRepository) : base(mapper, messageRepository)
    {
        _mapper = mapper;
        _messageRepository = messageRepository;
    }

    public async Task<List<ChatModel>> GetChatsByUserIdAsync(Guid id)
    {
        return await _messageRepository.GetAllChatsByUserIdAsync(id);
    }
    
    public async Task<List<Message>> GetMessagesByChatId(string chatId)
    {
        return await _messageRepository.GetMessagesByChatId(chatId);
    }
    
    
}