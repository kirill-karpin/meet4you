using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Message;

public class FilterCondition<T> : Dictionary<string, string>
{
    public void BuildQuery(IQueryable<T> queryable) 
    {
        foreach(var condition in this)
        {
            Console.WriteLine($"key: {condition.Key}  value: {condition.Value}");
            string key = condition.Key; 
            char[] removedchar = {'=','>','<'};
            
            Type type = typeof(Message);
            PropertyInfo? p = type.GetProperty(key.TrimStart(removedchar));

            var converter = TypeDescriptor.GetConverter(p);
            var result = converter.ConvertFrom(condition.Value);
            
            var prmtr = Expression.Parameter(type);
            
            var value = Expression.Constant(result);

            if (p != null)
            {
                if (key.Contains('='))
                {
                    var comprasion = Expression.Equal(Expression.Property(prmtr, p), value);
                    
                    var expr = Expression.Lambda<Func<T, bool>>(comprasion, prmtr);
                    queryable = queryable.Where(expr);
                }
            }
        }
    }
}