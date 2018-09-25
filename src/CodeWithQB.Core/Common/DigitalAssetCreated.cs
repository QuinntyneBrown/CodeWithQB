using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class DigitalAssetCreated: DomainEvent
    {
        public DigitalAssetCreated(Guid digitalAssetId, string name, byte[] bytes, string contentType)
        {
            DigitalAssetId = digitalAssetId;
            Name = name;
            Bytes = bytes;
            ContentType = contentType;
        }
        public Guid DigitalAssetId { get; set; }
        public string Name { get; set; }
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
    }
}
