using FileStorage.BO;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FileStorage;

class MongoFileUploadAdapter : IFileUploadAdapter
{
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDB;

    public MongoFileUploadAdapter()
    {
        MongoClient client = new MongoClient("mongodb://mongoadmin:meetforyou@localhost:27017/?authSource=admin");
        _mongoClient = client;
        _mongoDB = client.GetDatabase("meetforyou_files");
    }

    public Task<string> SaveFileAsync(UserFile file)
    {
        var uFiles = _mongoDB.GetCollection<UserFile>("user_files");
        uFiles.InsertOne(file);
        return Task.FromResult(file.Id.ToString());
    }

    public Task<bool> RemoveFileByIdAsync(string Id)
    {
        throw new NotImplementedException();
    }

    public Task<UserFile> GetFileByIdAsync(string Id)
    {
        var collectionMongo = _mongoDB.GetCollection<UserFile>("user_files");
        
        var filter = new BsonDocument()
        {
            { "_id", new BsonObjectId( new ObjectId(Id)) }
        };
        var collectionResult = collectionMongo.Find(filter);
        if (collectionResult.CountDocuments() == 0)
        {
            throw new Exception("No file exist");
        }
        
        var tFile = collectionResult.First();
        
        return Task.FromResult(tFile);
    }
}