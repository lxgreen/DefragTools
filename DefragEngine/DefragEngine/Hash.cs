using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DefragEngine
{
    public class Hash
    {
        private MD5 _md5;

        public byte[] Value { get; private set; }

        private Hash()
        {
            _md5 = MD5.Create();
        }

        public Hash(byte[] data) : this()
        {
            Value = _md5.ComputeHash(data);
        }

        public Hash(string data) : this()
        {
            var inputBytes = System.Text.Encoding.Unicode.GetBytes(data);
            Value = _md5.ComputeHash(inputBytes);
        }

        public Hash(Stream data) : this()
        {            
            Value = _md5.ComputeHash(data);
        }

        public static implicit operator Hash(string hash)
        {
            var result = new Hash();
            result.Value = FromString(hash);
            return result;
        }

        public static implicit operator Hash(byte[] hash)
        {
            var result = new Hash();
            result.Value = hash;
            return result;
        }       

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Value.Length; i++)
            {
                sb.Append(Value[i].ToString("X2"));
            }
            return sb.ToString().ToLower();
        }

        private static byte[] FromString(string hash)
        {
            List<byte> result = new List<byte>();

            for (int i = 0; i < hash.Length; i += 2)
            {
                byte parsed;
                if(byte.TryParse(string.Format("{0}{1}", hash[i], hash[i+1]), NumberStyles.HexNumber, null, out parsed))
                {
                    result.Add(parsed);
                }
            }

            return result.ToArray();
        }

        //private static string FileMD5(string path)
        //{
        //    string result = null;

        //    if (!File.Exists(Path.GetFullPath(path))) { return result; }

        //    using (var md5 = System.Security.Cryptography.MD5.Create())
        //    {
        //        using (var stream = File.OpenRead(Path.GetFullPath(path)))
        //        {
        //            var hash = md5.ComputeHash(stream);
        //            result = MD5HashToString(hash);
        //        }
        //    }

        //    return result;
        //}
    }
}