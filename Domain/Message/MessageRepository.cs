using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using Infrastructure;
using Message.Abstraction;
using Message.Models;
using Microsoft.EntityFrameworkCore;


namespace Message;

public class MessageRepository : CrudRepository<Message>, IMessageRepository
{
    public MessageRepository(DbContext context) : base(context)
    {
    }

    public async Task<List<Message>> GetListAsync(FilterCondition<Message> filterCondition, int page, int itemsPerPage)
    {
        var query = GetAll().AsQueryable();
        
        filterCondition.BuildQuery(query);

        query = query
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage);

        return await query.ToListAsync();
    }
    
    public async Task<List<ChatModel>> GetAllChatsByUserIdAsync(Guid userId)
    {
        var query = GetAll()
            .Where(message => message.From == userId || message.To == userId)
            .GroupBy(m => m.ChatId);

        var dbrGroup =  await query.Select(m => m.First()).ToListAsync();
            
       var dbr = dbrGroup.Select(m => new ChatModel
        {
            ChatId = m.ChatId,
            UserId = m.To != userId ? m.To : m.From,
        }).ToList();
        
        
        return dbr;
    }
    
    public async Task<List<Message>> GetMessagesByChatId(string chatId)
    {
        var query = GetAll()
            .Where(message => message.ChatId == chatId);
        
        return await query.ToListAsync();
    }
    
}