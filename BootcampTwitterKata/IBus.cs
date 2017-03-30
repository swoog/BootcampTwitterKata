namespace BootcampTwitterKata
{
    using System;

    public interface IBus
    {
        void Subcribe(Action<object> receiver);

        void Publish(object message);
    }
}