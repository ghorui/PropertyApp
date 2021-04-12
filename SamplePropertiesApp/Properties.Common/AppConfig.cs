using Microsoft.Extensions.Configuration;
using System.IO;

namespace Properties.Common
{
    public class AppConfig
    {
        private static IConfiguration Configuration;
        static AppConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public static IConfigurationSection Get(string name)
        {
            IConfigurationSection appSettings = Configuration.GetSection(name);
            return appSettings;
        }
    }
}
