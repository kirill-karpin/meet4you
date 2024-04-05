using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FileStorage.BO;
using System.Security.Cryptography;

public class UserFile
{
    
    private byte[] _file;
    private byte[] _hash;

    [BsonId]
    public ObjectId Id { get; set; }
    public string Hash { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    
    public byte[] File
    {
        get { return _file;}
        set
        {
            _file = value;
            var sha = new SHA256Managed();
            _hash = sha.ComputeHash(_file);
            Hash = System.Text.Encoding.UTF8.GetString(_hash);
        }
    }


}