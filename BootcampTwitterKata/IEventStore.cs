namespace BootcampTwitterKata
{
    using System.Collections.Generic;

    public interface IEventStore
    {
        List<object> Events { get; }

        void Insert(object obj);

        void Save();
    }
}