using NSubstitute;
using NUnit.Framework;

namespace BootcampTwitterKata.Tests
{
    public class Part_9_LoadSaveEventsToJson
    {
        private EventStore eventStore;
        private IFile file;
        private IEventsLoader eventsLoader;
        private IBus bus;

        public class FakeMessage
        {
            public string Title { get; set; }
        }

        [SetUp]
        public void Setup()
        {
            file = Substitute.For<IFile>();
            var injecter = new Injecter();
            bus = Substitute.For<IBus>();
            injecter
                .Bind<IBus>(bus)
                .Bind<IFile>(file)
                .Bind<IEventsLoader, EventsLoader>();

            eventStore = injecter.Get<EventStore>();
            eventsLoader = injecter.Get<IEventsLoader>();
        }

        [Test]
        public void Should_save_to_file_When_save_event_store()
        {
            eventStore.Insert(new FakeMessage { Title = "My title" });

            eventStore.Save();

            file.Received(1).Save(@"[{""$type"":""BootcampTwitterKata.Tests.Part_9_LoadSaveEventsToJson+FakeMessage, BootcampTwitterKata.Tests"",""Title"":""My title""}]");
        }

        [Test]
        public void Should_load_file_When_load_events()
        {
            file.Load().Returns(@"[{""$type"":""BootcampTwitterKata.Tests.Part_9_LoadSaveEventsToJson+FakeMessage, BootcampTwitterKata.Tests"",""Title"":""My title""}]");

            eventsLoader.Load();

            file.Received(1).Load();
        }

        [Test]
        public void Should_publish_to_bus_When_load_events()
        {
            file.Load().Returns(@"[{""$type"":""BootcampTwitterKata.Tests.Part_9_LoadSaveEventsToJson+FakeMessage, BootcampTwitterKata.Tests"",""Title"":""My title""}]");

            eventsLoader.Load();

            bus.Received(1).Publish(Arg.Any<FakeMessage>());
        }
    }
}