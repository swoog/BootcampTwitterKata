using BootcampTwitterKata.Messages;

namespace BootcampTwitterKata.Tests
{
    using NFluent;

    using NUnit.Framework;

    public class Part_4_CountProjection
    {
        [Test]
        public void Should_increment_When_received_insert_tweet()
        {
            var countProjection = new CountProjection();

            countProjection.Received(new InsertTweet(1, "Event 1"));

            Check.That(countProjection.Count).IsEqualTo(1);
        }

        [Test]
        public void Should_equal_to_twoo_When_received_insert_twoo_tweet()
        {
            var countProjection = new CountProjection();

            countProjection.Received(new InsertTweet(1, "Event 1"));
            countProjection.Received(new InsertTweet(2, "Event 2"));

            Check.That(countProjection.Count).IsEqualTo(2);
        }

        [Test]
        public void Should_increment_and_decrement_When_received_insert_and_delete_tweet()
        {
            var countProjection = new CountProjection();

            countProjection.Received(new InsertTweet(1, "Event 1"));
            countProjection.Received(new InsertTweet(2, "Event 2"));
            countProjection.Received(new DeleteTweet(1));

            Check.That(countProjection.Count).IsEqualTo(1);
        }
    }
}