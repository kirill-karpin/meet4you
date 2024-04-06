namespace WebApp.Hubs;

public class ConnectionPool
{
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
    
    public bool Remove(string id)
    {
        _pool.Remove(id);
        
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
}