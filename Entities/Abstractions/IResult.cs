using Entities.Abstractions;

namespace WebApp.Models.Abstraction;

public interface IResult
{
    public ICollection<IError>? GetErrors();
    
    public ICollection<string>? GetMessage();

    public bool IsSuccess();
}