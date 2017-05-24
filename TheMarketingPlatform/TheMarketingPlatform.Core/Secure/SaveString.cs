using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core.Secure
{
    public class SaveString
    {
        public SecureString SecureString { get; private set; }

        public string Value
        {
            get => ConvertToString(SecureString);
            set => SecureString = ConvertToSecureString(value);
        }

        public SaveString() => SecureString = new SecureString();
        public SaveString(SecureString value) => SecureString = value;
        public SaveString(string value) => SecureString = ConvertToSecureString(value);

        private SecureString ConvertToSecureString(string value)
        {
            var secureString = new SecureString();

            foreach (var item in value)
                secureString.AppendChar(item);

            return secureString;
        }

        private string ConvertToString(SecureString secureString) => Marshal.PtrToStringUni(
                Marshal.SecureStringToGlobalAllocUnicode(secureString));
    }
}
