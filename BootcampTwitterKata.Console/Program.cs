using System.Collections.Generic;
using System.Linq;
using BootcampTwitterKata.Helpers;

namespace BootcampTwitterKata.Console
{
    using System;

    using Newtonsoft.Json;

    using Ninject;

    public static class Program
    {
        private static int id = 1;

        private static IList<Tweet> tweets;

        public static void Main()
        {
            var kernel = GetKernel();
            var eventStore = kernel.Get<IEventStore>();
            var twitterCommands = kernel.Get<ITwitterCommands>();
            var twitterQuery = kernel.Get<ITwitterQuery>();

            var eventLoader = kernel.Get<IEventsLoader>();
            eventLoader.Load();

            if (twitterQuery.GetAll().Any())
            {
                id = twitterQuery.GetAll().Max(t => t.Id) + 1;
            }

            string line;
            do
            {
                Console.WriteLine("Count tweets : {0} like : {1}", twitterQuery.CountAll(), twitterQuery.CountLike());
                tweets = twitterQuery.GetAll();
                foreach (var tweet in tweets)
                {
                    WriteTweet(tweet);
                }

                Console.WriteLine("Options :");
                Console.WriteLine("\t1 - Insert tweet");
                Console.WriteLine("\t2 - Delete tweet");
                Console.WriteLine("\t3 - Like tweet");
                Console.WriteLine("\t4 - Unlike tweet");
                Console.WriteLine("\t5 - Display event store");

                line = Console.ReadLine();
                switch (line)
                {
                    case "1":
                        InsertTweet(twitterCommands);
                        break;
                    case "2":
                        DeleteTweet(twitterCommands);
                        break;
                    case "3":
                        LikeTweet(twitterCommands);
                        break;
                    case "4":
                        UnLikeTweet(twitterCommands);
                        break;
                    case "5":
                        DisplayEventStore(eventStore);
                        break;
                }
            }
            while (!string.IsNullOrEmpty(line));

            eventStore.Save();
        }

        private static IKernel GetKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IFile>().To<FileIO>().InSingletonScope();
            kernel.Bind<IEventsLoader>().To<EventsLoader>();

            kernel.Bind<IBus>().To<Bus>().InSingletonScope();

            kernel.Bind<IEventStore>().To<EventStore>().InSingletonScope();

            kernel.Bind<ITwitterCommands>().To<TwitterCommands>();
            kernel.Bind<ITwitterQuery>().To<TwitterQuery>();

            kernel.Bind<CountProjection>().ToSelf();
            kernel.Bind<LikeCountProjection>().ToSelf();
            kernel.Bind<ListProjection>().ToSelf();

            return kernel;
        }

        private static void DisplayEventStore(IEventStore eventStore)
        {
            foreach (var message in eventStore.Events)
            {
                Console.WriteLine(JsonConvert.SerializeObject(message, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects }));
            }
        }

        private static void WriteTweet(Tweet tweet)
        {
            Console.WriteLine(tweet.Like ? "\t {0}, {1}, like" : "\t {0}, {1}", tweet.Id, tweet.Title);
        }

        private static void UnLikeTweet(ITwitterCommands twitterCommands)
        {
            Console.Write("Tweet to unlike : ");
            twitterCommands.UnLike(ConsoleReadInt());
        }

        private static void LikeTweet(ITwitterCommands twitterCommands)
        {
            Console.Write("Tweet to like : ");
            twitterCommands.Like(ConsoleReadInt());
        }

        private static void InsertTweet(ITwitterCommands twitterCommands)
        {
            Console.Write("Title : ");
            var title = Console.ReadLine();
            twitterCommands.Insert(id++, title);
        }

        private static void DeleteTweet(ITwitterCommands twitterCommands)
        {
            Console.Write("Tweet to delete : ");
            twitterCommands.Delete(ConsoleReadTweet());
        }

        private static Tweet ConsoleReadTweet()
        {
            var id = ConsoleReadInt();
            return tweets.FirstOrDefault(t => t.Id == id);
        }

        private static int ConsoleReadInt()
        {
            return int.Parse(Console.ReadLine());
        }
    }
}