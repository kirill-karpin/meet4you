using Entities.Abstractions;

namespace WebApp.Models.Abstraction;

public class Result : IResult
{
    private ICollection<IError>? _errors = new List<IError>();
    public bool Success { set; get; } = true;

    public Dictionary<string, object> _data = new Dictionary<string, object>();
    
    public ICollection<string>? Messages { set; get; } = new List<string>();

    public ICollection<IError>? Errors
    {
        set
        {
            _errors = value;
            Success = false;
        }
        get => _errors;
    }

    public ICollection<IError>? GetErrors()
    {
        return _errors;
    }

    public ICollection<string>? GetMessage()
    {
        return Messages;
    }

    public bool IsSuccess()
    {
        return Success;
    }

    public void SetData(string key, object   value)
    {
        _data.Add(key, value);
    }
}