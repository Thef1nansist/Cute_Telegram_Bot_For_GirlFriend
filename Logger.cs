using System.IO;

namespace BotTelega
{
    public static class Logger
    {
        public static void Information(string message, string fileName)
        {
            if (!Directory.Exists(@".\LogFiles"))
            {
                Directory.CreateDirectory(@".\LogFiles");
            }
            string path = $@".\LogFiles\{fileName}.log";
            using var writer = new StreamWriter(path, true);
            writer.WriteLine(message);
        }
    }
}
