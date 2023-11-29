using Infrastructure;
using Message.Abstraction;
using Microsoft.EntityFrameworkCore;


namespace Message;

public class MessageRepository : CrudRepository<Message>, IMessageRepository
{
    public MessageRepository(DbContext context) : base(context)
    {
    }
}