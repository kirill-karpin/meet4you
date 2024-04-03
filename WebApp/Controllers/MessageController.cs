using Message;
using Message.Abstraction;
using Message.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User;
using WebApp.Models;
using IResult = WebApp.Models.Abstraction.IResult;

namespace WebApp.Controllers;

[ApiController]
[Route("/api/message")]
public class MessageController : Controller
{
    private readonly IMessageService _messageService;
    private readonly IUserService _userService;

    public MessageController(IMessageService messageService, IUserService userService)
    {
        _messageService = messageService;
        _userService = userService;
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
    
    [HttpGet]
    [Route("chats")]
    public async Task<IEnumerable<dynamic>> GetChatsAsync()
    {
        string id = User.Claims.First(i => i.Type == "Id").Value;
        var dbResult = await _messageService.GetChatsByUserIdAsync(new Guid(id));
   
        var result = dbResult.Join(
            _userService.GetAll(), 
            messages=> messages.UserId, 
            users => users.Id,
            (messages, users) => new
            { 
                ChatId = messages.ChatId,
                UserId = messages.UserId,
                UserName = users.UserName,
                LastName = users.LastName,
                FirstName = users.FirstName,
                City = String.Empty,
                Photo = String.Empty
            });

        return result.ToList();
    }
    
    [HttpGet]
    [Route("chat/{UserGuid}/items")]
    public async Task<List<Message.Message>> GetChatsMessagesAsync(Guid userGuid)
    {
        string id = User.Claims.First(i => i.Type == "Id").Value;

        var chatId = CreatingMessageDto.GetChatId(userGuid.ToString(), id);
        
        var dbResult = await _messageService.GetMessagesByChatId(chatId);
   
        return dbResult.ToList();
    }
}