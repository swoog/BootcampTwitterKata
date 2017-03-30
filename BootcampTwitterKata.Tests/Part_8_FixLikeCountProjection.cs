using BootcampTwitterKata.Messages;
using NSubstitute;
using NUnit.Framework;

namespace BootcampTwitterKata.Tests
{
    public class Part_8_FixLikeCountProjection
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
        public void Should_publish_message_unlike_When_delete_tweet()
        {
            this.twitterCommands.Delete(new Tweet { Id = 1, Like = true });

            this.bus.Received(1).Publish(Arg.Is<UnLikeTweet>(i => i.Id == 1));
        }
    }
}