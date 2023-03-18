using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Serilog;
using Utils;

namespace DAL;
public class DBContext {
    public DBContext(){
        log.Debug("Connecting to Database");
        Task tks = Connect();
        tks.Wait();
    }
    ILogger log = Logger.ContextLog<DBContext>();
    MongoClient Client;
    public IMongoDatabase DataBase { get; set; }
    public GridFSBucket GridFSBucket { get; private set; }
    public bool IsConnected { get { return DataBase != null; } }
    public async Task Connect(){
        SettingsReader reader = new SettingsReader();
        DBSettings settings = reader.GetSettings<DBSettings>("MongoDbSettings");
        MongoClientSettings clientsettings = new MongoClientSettings();
        
        clientsettings.Server = new MongoServerAddress(settings.Server, settings.Port);
        if (!string.IsNullOrEmpty(settings.Username) && !string.IsNullOrEmpty(settings.Password)){
            clientsettings.Credential = MongoCredential.CreateCredential("admin", settings.Username, settings.Password);
        }
        Client = new MongoClient(clientsettings);
        DataBase = Client.GetDatabase(settings.DatabaseName);
        GridFSBucket = new GridFSBucket(DataBase);

        // https://mongodb.github.io/mongo-csharp-driver/2.13/reference/gridfs/gettingstarted/
        if (DataBase != null) {
            log.Information("Successfully connected to Mongo DB " + settings.Server + ":" + settings.Port);
        }
        else {
            log.Fatal("Could not connect to Mongo DB " + settings.Server + ":" + settings.Port);
        }
    }
    public async Task Disconnect(){}
}