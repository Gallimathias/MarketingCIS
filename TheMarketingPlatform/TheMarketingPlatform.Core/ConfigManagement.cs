using TheMarketingPlatform.Core.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core
{
    /// <summary>
    /// Manager for basic settings, is fed from the config file
    /// </summary>
    public class ConfigManagement : Dictionary<string, object>
    {
        /// <summary>
        /// Gets the current config
        /// </summary>
        protected Config Config { get => config; set => config = value; }

        private Config config;
        private FileInfo configFile;

        /// <summary>
        /// Handles config management events
        /// </summary>
        /// <param name="key">The Setting</param>
        /// <param name="value">Value of the setting</param>
        public delegate void ConfigManagementEventHandler(string key, object value);
        /// <summary>
        /// Throw if the Value of a setting has changed
        /// </summary>
        public event ConfigManagementEventHandler ValueHasChanged;

        /// <summary>
        /// Manager for basic settings, is fed from the config file
        /// </summary>
        public ConfigManagement()
        {
            ValueHasChanged += ConfigManagement_ValueHasChanged;
        }
               
        /// <summary>
        /// Gets or set a setting
        /// </summary>
        /// <param name="key">The Setting</param>
        /// <returns>The Value of the setting</returns>
        public new object this[string key]
        {
            get => base[key];
            set
            {
                var oldValue = base[key];
                var newValue = TryToConvert(value, oldValue);
                base[key] = newValue;
                if (oldValue != newValue)
                    ValueHasChanged?.Invoke(key, newValue);
            }
        }

        /// <summary>
        /// Gets the value of a setting with converting in target type
        /// </summary>
        /// <typeparam name="T">The target type</typeparam>
        /// <param name="key">The setting</param>
        /// <returns>The converted value</returns>
        public T GetValue<T>(string key) => TryToConvert<T>(this[key]);

        /// <summary>
        /// Loads a Config file
        /// </summary>
        /// <param name="fullName">The full name of the config file</param>
        /// <returns>This returns a tubel. Loaded is true if all fine and false when theres a error. Exception is null is loading ok</returns>
        public (bool loaded, Exception exception) Load(string fullName)
        {
            configFile = new FileInfo(fullName);

            if (!configFile.Exists)
                return (false, new FileNotFoundException());

            try
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(fullName));

                foreach (var setting in config.ToList())
                    Add(setting.Key, setting.Value);

            }
            catch (Exception ex)
            {
                return (false, ex);
            }

            return (true, null);
        }

        /// <summary>
        /// Saves all changes to the current file
        /// </summary>
        public void ApplyChangesToFile() =>
            File.WriteAllText(configFile.FullName, JsonConvert.SerializeObject(config, Formatting.Indented));
        
        private object TryToConvert(object value, Type type) => Convert.ChangeType(value, type);
        private object TryToConvert(object value, object oldValue) => TryToConvert(value, oldValue.GetType());
        private T TryToConvert<T>(object value) => (T)TryToConvert(value, typeof(T));

        private void ConfigManagement_ValueHasChanged(string key, object value)
        {
            config.Settings[key] = value;
        }
    }
}
