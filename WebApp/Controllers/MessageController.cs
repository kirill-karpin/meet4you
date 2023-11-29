using Message;
using Message.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class MessageController : Controller
{
    private readonly MessageService _messageService;

    public MessageController(MessageService messageService)
    {
        _messageService = messageService;
    }

    // GET
    [HttpGet("{id}")]
    public async Task<MessageDto> Get(Guid id)
    {
        return await _messageService.GetByIdAsync(id);
    }
}