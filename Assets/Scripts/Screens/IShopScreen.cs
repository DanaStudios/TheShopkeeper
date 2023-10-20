using System;
using System.Collections.Generic;
using AppData.Shopkeeper;
using Item;

namespace Screens
{
	public interface IShopScreen : IScreen
	{
		event Action<IItem> BuyButtonPressed;
		void UpdateItemList(IShopkeeperData data, List<IItem> items);
	}
}