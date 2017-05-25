using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core.Secure
{
    /// <summary>
    /// A scure string implementation with a way back
    /// </summary>
    public class SaveString
    {
        /// <summary>
        /// The base secure string
        /// </summary>
        public SecureString SecureString { get; private set; }

        /// <summary>
        /// The converted value from secure string
        /// </summary>
        public string Value
        {
            get => ConvertToString(SecureString);
            set => SecureString = ConvertToSecureString(value);
        }

        /// <summary>
        /// A scure string implementation with a way back
        /// </summary>
        public SaveString() => SecureString = new SecureString();
        /// <summary>
        /// A scure string implementation with a way back. Converts a existing secure string to save string
        /// </summary>
        /// <param name="value">a existing secure string</param>
        public SaveString(SecureString value) => SecureString = value;
        /// <summary>
        /// A scure string implementation with a way back. Converts a string to a save string
        /// </summary>
        /// <param name="value">a string to save</param>
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
