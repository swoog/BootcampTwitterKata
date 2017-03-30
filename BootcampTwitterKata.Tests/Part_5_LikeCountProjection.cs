using BootcampTwitterKata.Messages;

namespace BootcampTwitterKata.Tests
{
    using NFluent;

    using NUnit.Framework;

    public class Part_5_LikeCountProjection
    {
        [Test]
        public void Should_increment_When_received_like_tweet()
        {
            var likeCountProjection = new LikeCountProjection();

            likeCountProjection.Received(new LikeTweet(1));

            Check.That(likeCountProjection.Count).IsEqualTo(1);
        }

        [Test]
        public void Should_equal_to_twoo_When_received_like_twoo_tweet()
        {
            var countProjection = new LikeCountProjection();

            countProjection.Received(new LikeTweet(1));
            countProjection.Received(new LikeTweet(2));

            Check.That(countProjection.Count).IsEqualTo(2);
        }

        [Test]
        public void Should_increment_and_decrement_When_received_insert_and_delete_tweet()
        {
            var countProjection = new LikeCountProjection();

            countProjection.Received(new LikeTweet(1));
            countProjection.Received(new LikeTweet(2));
            countProjection.Received(new UnLikeTweet(1));

            Check.That(countProjection.Count).IsEqualTo(1);
        }
    }
}