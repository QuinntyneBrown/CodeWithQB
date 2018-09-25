using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class InventoryItem: Entity
    {
        public InventoryItem(string name)
            => Apply(new InventoryItemCreated(InventoryItemId, name));

        public Guid InventoryItemId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }        
		public InventoryItemStatus Status { get; set; }
		public int Version { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case InventoryItemCreated inventoryItemCreated:                    
					Status = InventoryItemStatus.Active;
                    break;

                case InventoryItemNameChanged inventoryItemNameChanged:                    
					Version++;
                    break;

                case InventoryItemRemoved inventoryItemRemoved:
                    Status = InventoryItemStatus.InActive;
					Version++;
                    break;
            }
        }
        
        public void Remove()
            => Apply(new InventoryItemRemoved());
    }

    public enum InventoryItemStatus
    {
        Active,
        InActive
    }
}
