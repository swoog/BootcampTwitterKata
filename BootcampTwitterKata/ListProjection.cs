using BootcampTwitterKata.Messages;

namespace BootcampTwitterKata
{
    using System.Collections.Generic;
    using System.Linq;

    public class ListProjection
    {
        public List<Tweet> Tweets { get; set; }

        public void Received(object obj)
        {
        }
    }
}