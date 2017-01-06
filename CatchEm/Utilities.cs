using System;
using System.IO;
using System.Reflection;

namespace CatchEm
{
    public static class Utilities
    {
        public static string ReadEmbeddedResource(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "CatchEm." + path;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
                return result;
            }
        }
    }
}
