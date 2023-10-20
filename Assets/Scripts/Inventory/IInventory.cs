using System;
using System.Collections.Generic;
using Item;

namespace Inventory
{
	public interface IInventory
	{
		List<IItem> Items { get; }
		event Action Updated;
		IItem AddItem(IItem item);
		IItem RemoveItem(IItem item);
	}
}