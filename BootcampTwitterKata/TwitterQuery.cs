namespace BootcampTwitterKata
{
    using System.Collections.Generic;

    public class TwitterQuery : ITwitterQuery
    {
        public int CountAll()
        {
            return -1;
        }

        public int CountLike()
        {
            return -1;
        }

        public IList<Tweet> GetAll()
        {
            return new List<Tweet>();
        }
    }
}