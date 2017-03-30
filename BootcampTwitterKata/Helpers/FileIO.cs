using System.IO;

namespace BootcampTwitterKata.Helpers
{
    public class FileIO : IFile
    {
        private const string TweetsFile = "tweets.json";

        public void Save(string content)
        {
            File.WriteAllText(TweetsFile, content);
        }

        public string Load()
        {
            if (File.Exists(TweetsFile))
            {
                return File.ReadAllText(TweetsFile);
            }
            else
            {
                return "[]";
            }
        }
    }
}