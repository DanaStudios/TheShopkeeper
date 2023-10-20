using System;
using AppData.Shopkeeper;
using Interactions;
using Inventory;
using Screens;
using UnityEngine;

namespace Shopkeeper
{
	public class ShopkeeperBehaviour : MonoBehaviour, IInteractable
	{
		private IShopkeeperData shopkeeperData;
		private IInventory inventory;
		private IShopScreen shopScreen;

		public void Inject(IInventory inv, IShopkeeperData data, IShopScreen screen)
		{
			shopkeeperData = data;
			inventory = inv;
			shopScreen = screen;
			OnInventoryUpdated();
			inventory.Updated += OnInventoryUpdated;
		}

		private void OnInventoryUpdated()
		{
			Debug.Log("Inventory updated!");
			shopScreen.UpdateItemList(shopkeeperData, inventory.Items);
		}

		public async void Interact(Action callback)
		{
			shopScreen.Show();
			await new WaitUntil(() => !shopScreen.Visible);
			callback?.Invoke();
		}
	}
}