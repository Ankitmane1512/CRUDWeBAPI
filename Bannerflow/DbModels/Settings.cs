using Microsoft.Extensions.Configuration;

namespace Bannerflow.DbModels
{
    public class Settings
    {
        public string ConnectiongString;
        public string Database;
        public IConfiguration IConfigurationRoot;
    }
}
