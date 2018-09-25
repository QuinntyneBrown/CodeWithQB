using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class DigitalAsset : Entity
    {
        public DigitalAsset(string name, byte[] bytes, string contentType)
            => Apply(new DigitalAssetCreated(DigitalAssetId, name, bytes, contentType));

        public Guid DigitalAssetId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
        public DigitalAssetStatus Status { get; set; }
        protected override void When(object @event)
        {
            switch(@event)
            {
                case DigitalAssetCreated digitalAssetCreated:
                    DigitalAssetId = digitalAssetCreated.DigitalAssetId;
                    Bytes = digitalAssetCreated.Bytes;
                    ContentType = digitalAssetCreated.ContentType;
                    Name = digitalAssetCreated.Name;
                    Status = DigitalAssetStatus.Active;
                    break;
            }            
        }

        protected override void EnsureValidState()
        {

        }
    }

    public enum DigitalAssetStatus
    {
        Active,
        InActive
    }
}
