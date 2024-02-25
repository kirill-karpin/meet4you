using Message;
using Message.Abstraction;
using Message.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using IResult = WebApp.Models.Abstraction.IResult;

namespace WebApp.Controllers;

[ApiController]
[Route("/api/message")]
public class MessageController : Controller
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    // GET
    [Authorize]
    [HttpPost]
    [Route("send")]
    public async Task<IResult> SendMessageToUser(CreateMessageModelDto createMessageModelDto)
    {
        //return await _messageService.GetByIdAsync(id);
        var result =  new ResponseResult();
        string id = User.Claims.First(i => i.Type == "Id").Value;
        
        var message = new CreatingMessageDto
        {
            From = new Guid(id),
            To = createMessageModelDto.To,
            Content = createMessageModelDto.Content,
            IsRead = false,
            
        };

        var dbResult = await _messageService.CreateAsync(message);

        result.SetData("Id", dbResult);
        
        return result;
    }
    
}