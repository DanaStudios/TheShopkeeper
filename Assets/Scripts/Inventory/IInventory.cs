using System;
using System.Collections.Generic;
using Items;

namespace Inventory
{
	public interface IInventory
	{
		List<IItem> Items { get; }
		event Action Updated;
		IItem[] GetItems(IItem item, int count);
		void AddItems(IItem[] items);
	}
}