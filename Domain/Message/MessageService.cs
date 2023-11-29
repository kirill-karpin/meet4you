using Message.Abstraction;
using AutoMapper;
using Infrastructure;
using Message.Dto;

namespace Message;

public class MessageService : CrudService<Message, CreatingMessageDto,UpdatingMessageDto, MessageDto>
{
    private readonly IMapper _mapper;
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMapper mapper, 
        IMessageRepository messageRepository) : base(mapper, messageRepository)
    {
    }
}