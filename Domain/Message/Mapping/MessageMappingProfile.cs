using AutoMapper;
using Message.Dto;
using Message.Models;


namespace Message.Mapping;

public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        CreateMap<Message, MessageDto>();
        CreateMap<MessageDto, MessageModel>();
        CreateMap<CreatingMessageModel, CreatingMessageDto>();
        CreateMap<UpdatingMessageModel, UpdatingMessageDto>();
    }
}