using Ninject;

namespace BootcampTwitterKata.Tests
{
    using NFluent;

    using NSubstitute;

    using NUnit.Framework;

    public class Part_1_BusTests
    {
        public class FakeMessage
        {
        }

        [Test]
        public void Should_subscribe_is_call_When_publish_a_message()
        {
            FakeMessage message = null;
            var bus = new Bus();
            bus.Subcribe(m => message = m as FakeMessage);

            bus.Publish(new FakeMessage());

            Check.That(message).IsNotNull().And.IsInstanceOf<FakeMessage>();
        }

        [Test]
        public void Should_all_subscribes_are_call_When_publish_a_message()
        {
            var countSubscribe = 0;
            var bus = new Bus();
            bus.Subcribe(m => countSubscribe++);
            bus.Subcribe(m => countSubscribe++);

            bus.Publish(new FakeMessage());

            Check.That(countSubscribe).IsEqualTo(2);
        }
    }

    public class Injecter
    {
        private StandardKernel standardKernel;

        public Injecter()
        {
            standardKernel = new StandardKernel();
        }

        public T Get<T>()
        {
            return this.standardKernel.Get<T>();
        }

        public Injecter Bind<T>(T bus)
        {
            this.standardKernel.Bind<T>().ToConstant(bus);
            return this;
        }

        public Injecter Bind<T, TTo>()
            where TTo : T
        {
            this.standardKernel.Bind<T>().To<TTo>();
            return this;
        }
    }
}
