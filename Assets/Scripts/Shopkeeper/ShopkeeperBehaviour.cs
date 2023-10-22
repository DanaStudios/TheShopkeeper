using System;
using AppData.Shopkeeper;
using Commands;
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
			OnInventoryUpdated();
			shopkeeperInventory.Updated += OnInventoryUpdated;
		}
		
		public bool CanAfford(int totalCost) => shopkeeperWallet?.CurrentGold >= totalCost;
		public void ReceiveItems(IItem[] items) => shopkeeperInventory.AddItems(items);
		public IItem[] GetItems(IItem item, int count) => shopkeeperInventory.GetItems(item, count);
		public void Spend(int gold) => shopkeeperWallet.Subtract(gold);
		public void Earn(int gold) => shopkeeperWallet.Add(gold);
		public async void OnInteract(IBuyer buyer, Action callback)
		{
			currentBuyer = buyer;
			OnInventoryUpdated();
			inventoryScreen.Show();
			await new WaitUntil(() => !inventoryScreen.Visible);
			callback?.Invoke();
		}

		private void OnInventoryUpdated()
		{
			inventoryScreen.UpdateUI(shopkeeperData, shopkeeperInventory.Items, buyClicked: OnBuyButtonPressed);	
		}

		private void OnBuyButtonPressed(IItem item, int count)
		{
			ICommand sellCommand = new SellCommand(this, currentBuyer, item, count);
			sellCommand.Execute();
		}
	}
}