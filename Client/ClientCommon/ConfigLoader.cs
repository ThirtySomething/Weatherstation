using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace net.derpaul.tf
{
    /// <summary>
    /// Generic class as base for config files
    /// </summary>
    /// <typeparam name="ConfigClass"></typeparam>
    public class ConfigLoader<ConfigClass> where ConfigClass : ConfigSaver, new()
    {
        /// <summary>
        /// Configs are singletons, this is the internal instance
        /// </summary>
        private static ConfigClass SingletonInstance;

        /// <summary>
        /// The filename of the config file
        /// </summary>
        private static readonly string FileName = GetConfigFilePath();

        /// <summary>
        /// Public access to the singleton instance
        /// </summary>
        public static ConfigClass Instance
        {
            get
            {
                if (SingletonInstance == null)
                {
                    SingletonInstance = LoadFromFile(FileName);
                }
                return SingletonInstance;
            }
        }

        /// <summary>
        /// Get path to the config file
        /// </summary>
        /// <returns></returns>
        protected static string GetConfigFilePath()
        {
            var assemblyDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
            var configFilePath = Path.Combine(assemblyDirectory, GetConfigFileName()) + Constants.ConfigFileExtenstion;
            return configFilePath;
        }

        /// <summary>
        /// Get the name for the config file
        /// </summary>
        /// <returns></returns>
        protected static string GetConfigFileName()
        {
            return typeof(ConfigClass).Name;
        }

        /// <summary>
        /// Load the config file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected static ConfigClass LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                var config = new ConfigClass();
                config.Save();
                return config;
            }

            var serializer = new XmlSerializer(typeof(ConfigClass));
            using (var reader = new StreamReader(FileName))
            {
                return (ConfigClass)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Save the config file
        /// </summary>
        public void Save()
        {
            using (var writer = new StreamWriter(FileName))
            {
                var serializer = new XmlSerializer(typeof(ConfigClass));
                serializer.Serialize(writer, this);
            }
        }

        /// <summary>
        /// Used to read the attributes to save
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttributes(typeof(XmlIgnoreAttribute), true).Length == 0)
                .OrderBy(p => p.Name)
                .ToList();

            var builder = new StringBuilder();
            foreach (var property in properties)
            {
                builder.Append(property.Name)
                    .Append(" = ")
                    .Append(property.GetValue(this))
                    .AppendLine();
            }

            return builder.ToString();
        }
    }
}