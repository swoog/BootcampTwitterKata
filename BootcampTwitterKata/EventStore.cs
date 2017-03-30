using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BootcampTwitterKata
{
    using System.Collections.Generic;

    public class EventStore : IEventStore
    {
        public List<object> Events { get; }

        public void Insert(object obj)
        {
        }

        public void Save()
        {
        }
    }
}