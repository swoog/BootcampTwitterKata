namespace BootcampTwitterKata
{
    public interface ITwitterCommands
    {
        void Insert(int id, string title);

        void Delete(Tweet id);

        void Like(int id);

        void UnLike(int id);
    }
}