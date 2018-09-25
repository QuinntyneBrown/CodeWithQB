using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class InventoryItemNameChanged: DomainEvent
    {
        public InventoryItemNameChanged(Guid inventoryItemNameChangedId, string name)
        {
            InventoryItemNameChangedId = inventoryItemNameChangedId;
            Name = name;
        }

		public Guid InventoryItemNameChangedId { get; set; }
        public string Name { get; set; }
    }
}
