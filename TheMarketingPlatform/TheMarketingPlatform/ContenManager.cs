using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TheMarketingPlatform
{
    internal static class ContenManager
    {
        public static Dictionary<string, Type> Pages { get; private set; }
        static ContenManager()
        {
            Pages = new Dictionary<string, Type>();

            var pages = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType == typeof(Page));

            foreach (var page in pages)
                Pages.Add(page.Name, page);
        }

        public static object GetPage(string name, params object[] args) => Activator.CreateInstance(Pages[name], args);
    }
}
