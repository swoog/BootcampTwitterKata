namespace BootcampTwitterKata
{
    public interface IFile
    {
        void Save(string content);

        string Load();
    }
}