using System.Text.Json;

namespace Common;

public class EventMessage
{
    public EventType Type;
    public List<string> Receivers = new List<string>();
    public EventSubscriber Subscriber { get; set; }

    public string DataJson { get; set; }

    public bool isBroadcast()
    {
        return Type == EventType.Broadcast;
    }

    public bool isPersonal()
    {
        return Type == EventType.Personal;
    }

    public static EventMessage GetBroadcastMessageFabricMethod(EventSubscriber subscriber, object data)
    {
        return new EventMessage()
        {
            Type = EventType.Broadcast,
            Subscriber = subscriber,
            DataJson =  JsonSerializer.Serialize(data)
        };
    }

    public static EventMessage GetPersonalMessageFabricMethod(EventSubscriber subscriber, string receiver, object data)
    {
        return new EventMessage()
        {
            Receivers = new List<string>() { receiver },
            Type = EventType.Personal,
            Subscriber = subscriber,
            DataJson = JsonSerializer.Serialize(data)
        };
    }
    
    public static EventMessage GetPersonalNotificationFabricMethod(string receiver, object data)
    {
        return new EventMessage()
        {
            Receivers = new List<string>() { receiver },
            Type = EventType.Personal,
            Subscriber = EventSubscriber.Notification,
            DataJson = JsonSerializer.Serialize(data)
        };
    }
}