namespace BootcampTwitterKata.Messages
{
    public class InsertTweet
    {
        public int Id { get; }

        public string Title { get; }

        public InsertTweet(int id, string title)
        {
            this.Id = id;
            this.Title = title;
        }
    }
}