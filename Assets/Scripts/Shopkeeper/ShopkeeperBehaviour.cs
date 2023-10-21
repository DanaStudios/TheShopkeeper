using System;
using AppData.Shopkeeper;
using Inventory;
using Item;
using Screens.Shop;
using Transactions;
using UnityEngine;
using Wallet;

namespace Shopkeeper
{
	public class ShopkeeperBehaviour : MonoBehaviour, IShopkeeper
	{
		private IShopkeeperData shopkeeperData;
		private IInventory shopkeeperInventory;
		private IWallet shopkeeperWallet;
		private IShopScreen shopScreen;
		
		private IBuyer currentBuyer;

		public void Inject(IShopkeeperData data, IInventory inv, IWallet wallet, IShopScreen screen)
		{
			shopkeeperData = data;
			shopkeeperInventory = inv;
			shopkeeperWallet = wallet;
			shopScreen = screen;
			
			shopkeeperInventory.Updated += OnInventoryUpdated;
			shopScreen.BuyButtonPressed += OnBuyButtonPressed;
			
			OnInventoryUpdated();
		}
		
		public async void OnInteract(IBuyer buyer, Action callback)
		{
			currentBuyer = buyer;
			shopScreen.Show();
			await new WaitUntil(() => !shopScreen.Visible);
			callback?.Invoke();
		}

		private void OnInventoryUpdated()
		{
			shopScreen.UpdateItemList(shopkeeperData, shopkeeperInventory.Items);
		}
		
		private void OnBuyButtonPressed(IItem item, int count)
		{
			if (currentBuyer == null) throw new Exception("[Shopkeeper] Buyer does not exist!");
			if (item == null) throw new Exception("[Shopkeeper] Item does not exist!");
			
			var totalCost = item.Cost * count;
			if (!currentBuyer.CanAfford(totalCost))
			{
				Debug.LogWarning("[Shopkeeper] You don't have enough gold!");
				return;
			}
			var itemsToGiveBuyer = shopkeeperInventory.GetItems(item, count);
			
			currentBuyer.Spend(totalCost);
			shopkeeperWallet.Add(totalCost);
			currentBuyer.Receive(itemsToGiveBuyer);
			
			Debug.Log($"{currentBuyer} bought {item.Name} (x{count}) for {totalCost} gold!");
		}
	}
}