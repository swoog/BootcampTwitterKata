namespace BootcampTwitterKata.Messages
{
    public class LikeTweet
    {
        public LikeTweet(int id)
        {
            this.Id = id;
        }

        public int Id { get; private set; }
    }
}