using System;
using AppData.Shopkeeper;
using Interactions;
using Screens;
using UnityEngine;

namespace Shopkeeper
{
	public class ShopkeeperBehaviour : MonoBehaviour, IInteractable
	{
		private IShopkeeperData shopkeeperData;
		private IScreen shopScreen;

		public void Inject(IShopkeeperData data, IScreen screen)
		{
			shopkeeperData = data;
			shopScreen = screen;
			shopScreen.Hide();
		}

		public async void Interact(Action callback)
		{
			shopScreen.Show();
			await new WaitUntil(() => !shopScreen.Visible);
			callback?.Invoke();
		}
	}
}