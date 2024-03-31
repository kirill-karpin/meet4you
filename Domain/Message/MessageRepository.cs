using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using Infrastructure;
using Message.Abstraction;
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
    
    public async Task<List<Message>> GetAllChatsByUserIdAsync(Guid userId)
    {
        var query = GetAll()
            .Where(message => message.From == userId)
            .GroupBy(m => m.From)
            .Select(g => g.First());
        return await query.ToListAsync();
    }
    
}