using BootcampTwitterKata.Helpers;

namespace BootcampTwitterKata.Tests
{
    using NFluent;

    using NUnit.Framework;

    public class Part_2_EventStore
    {
        private IEventStore eventStore;

        public class FakeEvent
        {
            private readonly int val;

            public FakeEvent(int val)
            {
                this.val = val;
            }

            public override bool Equals(object obj)
            {
                var fakeEvent = obj as FakeEvent;

                return this.val == fakeEvent.val;
            }

            public override int GetHashCode()
            {
                return 0;
            }
        }

        [SetUp]
        public void Setup()
        {
            var injecter = new Injecter();
            injecter.Bind<IFile, FileIO>().Bind<IEventStore, EventStore>();
            eventStore = injecter.Get<IEventStore>();
        }

        [Test]
        public void Shoudl_have_an_event_in_collection_When_insert_event()
        {
            eventStore.Insert("Event 1");

            Check.That(eventStore.Events).ContainsExactly("Event 1");
        }

        [Test]
        public void Shoudl_have_twoo_events_in_collection_When_insert_a_second_event()
        {
            eventStore.Insert("Event 1");

            eventStore.Insert("Event 2");

            Check.That(eventStore.Events).ContainsExactly("Event 1", "Event 2");
        }

        [Test]
        public void Shoudl_have_an_object_event_in_collection_When_insert_an_object()
        {
            eventStore.Insert(new FakeEvent(1));

            Check.That(eventStore.Events).ContainsExactly(new FakeEvent(1));
        }

        [Test]
        public void Shoudl_have_two_object_event_in_collection_When_object_are_equal()
        {
            eventStore.Insert(new FakeEvent(1));

            eventStore.Insert(new FakeEvent(1));

            Check.That(eventStore.Events).ContainsExactly(new FakeEvent(1), new FakeEvent(1));
        }
    }
}