namespace BootcampTwitterKata.Messages
{
    public class DeleteTweet
    {
        public DeleteTweet(int id)
        {
            this.Id = id;
        }

        public int Id { get; private set; }
    }
}