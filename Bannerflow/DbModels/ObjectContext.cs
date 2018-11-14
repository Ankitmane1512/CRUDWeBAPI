using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Bannerflow.Models;

namespace Bannerflow.DbModels
{
    public class ObjectContext
    {
        public IConfiguration Configuration { get; }
        public IMongoDatabase _database = null;


        public ObjectContext(IOptions<Settings> settings)
        {
            Configuration = settings.Value.IConfigurationRoot;
            settings.Value.ConnectiongString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
            settings.Value.Database = Configuration.GetSection("MongoConnection:Database").Value;
            var client = new MongoClient(settings.Value.ConnectiongString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);

        }

        public IMongoCollection<Banner> Banners
        {
            get
            {
                //Table Name
                return _database.GetCollection<Banner>("Banner");
            }

        }
    }
}
