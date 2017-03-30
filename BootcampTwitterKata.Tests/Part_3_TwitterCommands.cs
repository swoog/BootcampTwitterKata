using BootcampTwitterKata.Messages;

namespace BootcampTwitterKata.Tests
{
    using NSubstitute;

    using NUnit.Framework;

    public class Part_3_TwitterCommands
    {
        private IBus bus;

        private ITwitterCommands twitterCommands;

        private IEventStore eventStore;

        [SetUp]
        public void Setup()
        {
            this.bus = Substitute.For<IBus>();
            this.eventStore = Substitute.For<IEventStore>();
            var injecter = new Injecter();
            injecter
                .Bind<IBus>(this.bus)
                .Bind<IEventStore>(eventStore)
                .Bind<ITwitterCommands, TwitterCommands>();

            this.twitterCommands = injecter.Get<ITwitterCommands>();
        }

        [Test]
        public void Should_subscribe_to_event_store_When_create_twitter_commands()
        {
            this.bus.Received(1).Subcribe(this.eventStore.Insert);
        }

        [Test]
        public void Should_publish_message_event_When_insert_tweet()
        {
            this.twitterCommands.Insert(1, "Tweet 1");

            this.bus.Received(1).Publish(Arg.Is<InsertTweet>(i => i.Id == 1 && i.Title == "Tweet 1"));
        }

        [Test]
        public void Should_publish_message_event_When_delete_tweet()
        {
            this.twitterCommands.Delete(new Tweet { Id = 1 });

            this.bus.Received(1).Publish(Arg.Is<DeleteTweet>(i => i.Id == 1));
        }

        [Test]
        public void Should_publish_message_event_When_like_tweet()
        {
            this.twitterCommands.Like(1);

            this.bus.Received(1).Publish(Arg.Is<LikeTweet>(i => i.Id == 1));
        }

        [Test]
        public void Should_publish_message_event_When_unlike_tweet()
        {
            this.twitterCommands.UnLike(1);

            this.bus.Received(1).Publish(Arg.Is<UnLikeTweet>(i => i.Id == 1));
        }
    }
}