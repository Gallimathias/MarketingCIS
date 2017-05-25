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
    public class ConfigManagement : Dictionary<string, object>
    {
        private Config config;
        private FileInfo configFile;

        public delegate void ConfigManagementEventHandler(string key, object value);
        public event ConfigManagementEventHandler ValueHasChanged;

        public ConfigManagement()
        {
            ValueHasChanged += ConfigManagement_ValueHasChanged;
        }

        private void ConfigManagement_ValueHasChanged(string key, object value)
        {
            config.Settings[key] = value;
        }

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

        public T GetValue<T>(string key) => TryToConvert<T>(this[key]);

        private object TryToConvert(object value, Type type) => Convert.ChangeType(value, type);
        private object TryToConvert(object value, object oldValue) => TryToConvert(value, oldValue.GetType());
        private T TryToConvert<T>(object value) => (T)TryToConvert(value, typeof(T));


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

        public void ApplyChangesToFile() =>
            File.WriteAllText(configFile.FullName, JsonConvert.SerializeObject(config, Formatting.Indented));

    }
}
