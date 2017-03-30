using BootcampTwitterKata.Messages;

namespace BootcampTwitterKata.Tests
{
    using NFluent;

    using NUnit.Framework;

    public class Part_6_ListProjection
    {
        private ListProjection listProjection;

        [SetUp]
        public void Setup()
        {
            this.listProjection = new ListProjection();
        }

        [Test]
        public void Should_contains_a_tweet_When_received_insert_tweet()
        {
            this.listProjection.Received(new InsertTweet(1, "Tweet 1"));

            Check.That(this.listProjection.Tweets.Extracting("Title")).ContainsExactly("Tweet 1");
        }

        [Test]
        public void Should_contains_twoo_tweet_When_received_insert_tweet_twice()
        {
            this.listProjection.Received(new InsertTweet(1, "Tweet 1"));
            this.listProjection.Received(new InsertTweet(2, "Tweet 2"));

            Check.That(this.listProjection.Tweets.Extracting("Title")).ContainsExactly("Tweet 1", "Tweet 2");
        }

        [Test]
        public void Should_contains_zero_tweet_When_received_delete_tweet()
        {
            this.listProjection.Received(new InsertTweet(1, "Tweet 1"));
            this.listProjection.Received(new DeleteTweet(1));

            Check.That(this.listProjection.Tweets).IsEmpty();
        }

        [Test]
        public void Should_contains_only_second_tweet_When_received_delete_on_first_tweet()
        {
            this.listProjection.Received(new InsertTweet(1, "Tweet 1"));
            this.listProjection.Received(new InsertTweet(2, "Tweet 2"));
            this.listProjection.Received(new DeleteTweet(1));

            Check.That(this.listProjection.Tweets.Extracting("Title")).ContainsExactly("Tweet 2");
        }

        [Test]
        public void Should_like_first_tweet_When_received_like_on_first_tweet()
        {
            this.listProjection.Received(new InsertTweet(1, "Tweet 1"));
            this.listProjection.Received(new InsertTweet(2, "Tweet 2"));
            this.listProjection.Received(new LikeTweet(1));

            Check.That(this.listProjection.Tweets.Extracting("Like")).ContainsExactly(true, false);
        }

        [Test]
        public void Should_unlike_first_tweet_When_received_unlike_on_first_tweet()
        {
            this.listProjection.Received(new InsertTweet(1, "Tweet 1"));
            this.listProjection.Received(new InsertTweet(2, "Tweet 2"));
            this.listProjection.Received(new LikeTweet(1));
            this.listProjection.Received(new UnLikeTweet(1));

            Check.That(this.listProjection.Tweets.Extracting("Like")).ContainsExactly(false, false);
        }
    }
}