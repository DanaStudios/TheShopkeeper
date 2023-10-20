using System;
using System.Collections.Generic;
using Item;

namespace Inventory
{
	public class Inventory : IInventory
	{
		public List<IItem> Items { get; }
		public event Action Updated;
		private readonly int capacity;
		
		public Inventory(int capacity, IEnumerable<Item.Item> items)
		{
			Items = new List<IItem>(capacity);
			this.capacity = capacity;

			foreach (var item in items)
			{
				AddItem(item);
			}
		}
		
		public IItem AddItem(IItem item)
		{
			if (Items.Count >= capacity)
			{
				throw new InvalidOperationException("Cannot add more items, inventory is full.");
			}

			Items.Add(item);
			Updated?.Invoke();
			return item; 
		}

		public IItem RemoveItem(IItem item)
		{
			if (!Items.Remove(item))
			{
				throw new InvalidOperationException("Item not found in inventory.");
			}

			Updated?.Invoke();
			return item;
		}
	}
}