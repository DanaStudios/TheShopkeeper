using System;
using AppData.Shopkeeper;
using Inventory;
using Items;
using Screens.Inventory;
using Transactions;
using UnityEngine;
using Wallets;

namespace Shopkeeper
{
	public class ShopkeeperBehaviour : MonoBehaviour, IShopkeeper
	{
		private IShopkeeperData shopkeeperData;
		private IInventory shopkeeperInventory;
		private IWallet shopkeeperWallet;
		private IInventoryScreen inventoryScreen;
		
		private IBuyer currentBuyer;

		public void Inject(IShopkeeperData data, IInventory inv, IWallet wallet, IInventoryScreen screen)
		{
			shopkeeperData = data;
			shopkeeperInventory = inv;
			shopkeeperWallet = wallet;
			inventoryScreen = screen;
			
			shopkeeperInventory.Updated += OnInventoryUpdated;
			
			OnInventoryUpdated();
		}
		
		public async void OnInteract(IBuyer buyer, Action callback)
		{
			currentBuyer = buyer;
			inventoryScreen.Show();
			await new WaitUntil(() => !inventoryScreen.Visible);
			callback?.Invoke();
		}

		private void OnInventoryUpdated()
		{
			inventoryScreen.UpdateItemList(shopkeeperData, shopkeeperInventory.Items, buyClicked: OnBuyButtonPressed);
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