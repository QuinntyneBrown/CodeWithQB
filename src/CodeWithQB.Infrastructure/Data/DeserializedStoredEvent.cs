using CodeWithQB.Core.Models;
using System;
using static Newtonsoft.Json.JsonConvert;

namespace CodeWithQB.Infrastructure.Data
{
    public class DeserializedStoredEvent
    {
        public DeserializedStoredEvent(StoredEvent @event)
        {
            StoredEventId = @event.StoredEventId;
            StreamId = @event.StreamId;
            Type = @event.Type;
            Aggregate = @event.Aggregate;
            AggregateDotNetType = @event.AggregateDotNetType;
            Data = DeserializeObject(@event.Data, System.Type.GetType(@event.DotNetType));
            DotNetType = @event.DotNetType;
            CreatedOn = @event.CreatedOn;
            Version = @event.Version;
            Sequence = @event.Sequence;
        }
        public int Sequence { get; set; }
        public Guid StoredEventId { get; set; }
        public Guid StreamId { get; set; }
        public string Type { get; set; }
        public string Aggregate { get; set; }
        public string AggregateDotNetType { get; set; }
        public object Data { get; set; }
        public string DotNetType { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Version { get; set; }
    }
}
