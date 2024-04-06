namespace WebApp.Hubs;

public class ConnectionPool
{
    private readonly object _lock = new object();
    private Dictionary<string, List<string>> _pool = new();
    
    public bool Add(string id, string connectionId)
    {
        //TODO Add semaphore
        
        if (_pool.TryGetValue(id, out var list1))
        {
            list1.Add(connectionId);
        }
        else
        {
            var list = new List<string>();
            list.Add(connectionId);
            _pool.Add(id, list);
        }

        return true;
    }
    
    public bool Remove(string id, string connectionId)
    {
        if (_pool.ContainsKey(id))
        {
            _pool.TryGetValue(id, out  var list1);
            list1?.Remove(connectionId);
            if (list1?.Count == 0)
            {
                _pool.Remove(id);
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    public List<string> GetConnectionsByUserId(string id)
    {
        _pool.TryGetValue(id, out var list1);
        if (list1 != null)
        {
            return list1;
        }

        return new List<string>();
    }

    public int GetCount()
    {
        return _pool.Count; 
    }

    public void Update(UpdatePoolAction item)
    {
        lock (_lock)
        {
            if (item.IsDelete)
            {
                Remove(item.UserId,  item.ConnectionId);
            }
            else
            {
                Add(item.UserId, item.ConnectionId);
            }
        }
    }
}