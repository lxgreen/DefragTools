using System.Collections.Generic;
using System.IO;
using System.Text;
using Shell32;

namespace DefragEngine
{
    public class DefragEngineUtils
    {
        private const int EXTENDED_HEADER_COUNT = 280;

        public static Dictionary<string, string> GetExtendedProperties(string path)
        {
            Dictionary<string, string> properties = null;

            if (!File.Exists(path))
            {
                return properties;
            }

            var dirName = Path.GetDirectoryName(path);

            Shell shell = new Shell();
            Folder directory = shell.NameSpace(dirName);
            FolderItem folderItem = directory.ParseName(Path.GetFileName(path));

            Dictionary<int, string> headers = GetExtendedPropertyHeaders(directory);

            if (folderItem != null)
            {
                properties = new Dictionary<string, string>();
                for (int i = 0; i < EXTENDED_HEADER_COUNT + 1; i++)
                {
                    string property = directory.GetDetailsOf(folderItem, i);
                    if (!string.IsNullOrEmpty(property))
                    {
                        properties.Add(headers[i], property);
                    }
                }
            }

            return properties;
        }

        private static Dictionary<int, string> GetExtendedPropertyHeaders(Folder directory)
        {
            Dictionary<int, string> headers = new Dictionary<int, string>();

            for (int i = 0; i < EXTENDED_HEADER_COUNT + 1; i++)
            {
                string header = directory.GetDetailsOf(null, i);
                if (!string.IsNullOrEmpty(header))
                {
                    headers.Add(i, header);
                }
            }

            return headers;
        }

        public static Dictionary<int, string> GetExtendedPropertyHeaders(string path)
        {
            Dictionary<int, string> headers = null;

            var dirName = Path.GetDirectoryName(path);
            if (!Directory.Exists(dirName))
            {
                return headers;
            }

            Shell shell = new Shell();
            Folder directory = shell.NameSpace(dirName);

            headers = GetExtendedPropertyHeaders(directory);

            return headers;
        }

        
    }
}