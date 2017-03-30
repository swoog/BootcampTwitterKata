using System.Collections.Generic;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace BootcampTwitterKata.Tests
{
    public class Part_7_Query
    {
        private ITwitterQuery twitterQuery;
        private IBus bus;
        private CountProjection countProjection;
        private LikeCountProjection likeCountProjection;
        private ListProjection listProjection;

        [SetUp]
        public void Setup()
        {
            var injecter = new Injecter();
            bus = Substitute.For<IBus>();
            countProjection = new CountProjection { Count = 2 };
            likeCountProjection = new LikeCountProjection { Count = 1 };
            listProjection = new ListProjection {  Tweets  = new List<Tweet>{ new Tweet { Title = "Tweet 1" } , new Tweet { Title = "Tweet 2" } } };
            injecter
                .Bind<CountProjection>(countProjection)
                .Bind<LikeCountProjection>(likeCountProjection)
                .Bind<ListProjection>(listProjection)
                .Bind<IBus>(bus)
                .Bind<ITwitterQuery, TwitterQuery>();
            this.twitterQuery = injecter.Get<ITwitterQuery>();
        }

        [Test]
        public void Should_projection_tweets_count_When_query_counts()
        {
            var count = this.twitterQuery.CountAll();

            Check.That(count).IsEqualTo(2);
        }

        [Test]
        public void Should_projection_tweets_like_count_When_query_like_counts()
        {
            var count = this.twitterQuery.CountLike();

            Check.That(count).IsEqualTo(1);
        }

        [Test]
        public void Should_projection_all_tweets_When_query_all_tweets()
        {
            var count = this.twitterQuery.GetAll();

            Check.That(count.Extracting("Title")).ContainsExactly("Tweet 1", "Tweet 2");
        }

        [Test]
        public void Should_subscribe_to_count_projection_receiver_When_create_twitter_query()
        {
            this.bus.Received(1).Subcribe(this.countProjection.Received);
        }

        [Test]
        public void Should_subscribe_to_count_like_projection_receiver_When_create_twitter_query()
        {
            this.bus.Received(1).Subcribe(this.likeCountProjection.Received);
        }

        [Test]
        public void Should_subscribe_to_list_projection_receiver_When_create_twitter_query()
        {
            this.bus.Received(1).Subcribe(this.listProjection.Received);
        }
    }
}