namespace BootcampTwitterKata
{
    using System.Collections.Generic;

    public interface ITwitterQuery
    {
        int CountAll();

        int CountLike();

        IList<Tweet> GetAll();
    }
}