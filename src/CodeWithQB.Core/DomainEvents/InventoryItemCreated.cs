using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class InventoryItemCreated: DomainEvent
    {
        public InventoryItemCreated(Guid inventoryItemId, string name)
        {
            InventoryItemId = inventoryItemId;
            Name = name;
        }

		public Guid InventoryItemId { get; set; }
        public string Name { get; set; }
    }
}
