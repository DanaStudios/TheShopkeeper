using System;
using System.Linq;
using System.Collections.Generic;
using Items;

namespace Inventory
{
	public class Inventory : IInventory
	{
		public List<IItem> Items { get; }
		public event Action Updated;
		private readonly int capacity;
		
		public Inventory(int capacity, IEnumerable<Item> items)
		{
			Items = new List<IItem>(capacity);
			this.capacity = capacity;

			if (items == null) return;
			
			foreach (var item in items)
			{
				AddItem(item);
			}
		}
		
		public IItem[] GetItems(IItem item, int count)
		{
			if (Items.Count(i => i == item) != count)
			{
				throw new Exception($"Not enough {item.Name} to remove!");
			}

			var items = new IItem[count];
			for (var i = 0; i < count; i++)
			{
				items[i] = RemoveItem(item);
			}

			Updated?.Invoke();
			return items;
		}

		public void AddItems(IItem[] items)
		{
			foreach (var item in items) AddItem(item);
			Updated?.Invoke();
		}

		private void AddItem(IItem item)
		{
			if (Items.Count >= capacity)
			{
				throw new InvalidOperationException("Cannot add more items, inventory is full.");
			}

			Items.Add(item);
		}

		private IItem RemoveItem(IItem item)
		{
			if (!Items.Remove(item))
			{
				throw new InvalidOperationException("Item not found in inventory.");
			}

			return item;
		}
		
	}
}