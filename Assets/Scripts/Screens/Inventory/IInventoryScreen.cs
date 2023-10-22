using System;
using System.Collections.Generic;
using AppData.Character;
using Items;

namespace Screens.Inventory
{
	public interface IInventoryScreen : IScreen
	{
		void UpdateItemList(ICharacterData data, IEnumerable<IItem> items, Action<IItem, int> buyClicked = null, 
			Action<IItem, int> sellClicked = null, Action<IItem, int> useClicked = null);
	}
}