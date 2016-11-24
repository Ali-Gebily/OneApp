using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using OneApp.Common.Core.Attributes;
using OneApp.Common.Core.Models;

namespace OneApp.Common.Core.Utilities
{
    public static class OneAppUtility
    {
        public static IEnumerable<Type> GetOneAppTypesOfType<T>()
        {
            var type = typeof(T);
            var types = AppDomain.CurrentDomain.GetAssemblies().Where(ass => ass.GetCustomAttributes(typeof(OneAppAssemblyAttribute), false).Length > 0)
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            return types;
        }
        static char[] _validChars = {'2','3','4','5','6','7','8','9',
                   'A','B','C','D','E','F','G','H',
                   'J','K','L','M','N','P','Q',
                   'R','S','T','U','V','W','X','Y','Z'}; // len=32


        const int _codelength = 6; // lenth of passcode
        readonly static string _keyPrefix = DateTime.Now.DayOfYear.ToString();
        /// <summary>
        /// Willl generate random key and then hash using it, and return the code and hashkey
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static GenerateCodeModel GetCodeForValue(string value)
        {
            return GetCodeForValue(new Random().Next(1000000, 100000000).ToString(), value);
        }
        public static GenerateCodeModel GetCodeForValue(string keyInitialValue, string value)
        {
            var key = _keyPrefix + keyInitialValue;
            byte[] hash;
            using (HMACSHA1 sha1 = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(key)))
                hash = sha1.ComputeHash(UTF8Encoding.UTF8.GetBytes(value));
            int startpos = hash[hash.Length - 1] % (hash.Length - _codelength);
            StringBuilder passbuilder = new StringBuilder();
            for (int i = startpos; i < startpos + _codelength; i++)
                passbuilder.Append(_validChars[hash[i] % _validChars.Length]);
            return new GenerateCodeModel { KeyInitialValue = keyInitialValue, Value = value, Code = passbuilder.ToString() };
        }
    }
}

