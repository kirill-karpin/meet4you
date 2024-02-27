namespace FileStorage.BO;
using System.Security.Cryptography;

public class UserFile
{
    private byte[] _file;
    private byte[] _hash;
    
    public string Hash { get; set; }
    public DateTime creationDt { get; set; }
    public string source_name { get; set; }
    public int? seq_number { get; set; }
    public byte[] file
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