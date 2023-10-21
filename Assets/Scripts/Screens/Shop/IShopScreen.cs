using System;
using System.Collections.Generic;
using AppData.Shopkeeper;
using Item;

namespace Screens.Shop
{
	public interface IShopScreen : IScreen
	{
		event Action<IItem, int> BuyButtonPressed;
		void UpdateItemList(IShopkeeperData data, List<IItem> items);
	}
}