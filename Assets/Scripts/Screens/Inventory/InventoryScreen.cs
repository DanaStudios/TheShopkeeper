using System;
using System.Collections.Generic;
using System.Linq;
using AppData.Character;
using Extensions;
using Items;
using UnityEngine;
using UnityEngine.UIElements;

namespace Screens.Inventory
{
	public class InventoryScreen : IInventoryScreen
	{
		public bool Visible => RootVisualElement.style.display == DisplayStyle.Flex;
		public void Show() => RootVisualElement.style.display = DisplayStyle.Flex;
		public void Hide() => RootVisualElement.style.display = DisplayStyle.None;
		private VisualElement RootVisualElement => UIDocument.rootVisualElement;
		private UIDocument UIDocument { get; }

		public InventoryScreen(UIDocument uiDocument)
		{
			UIDocument = uiDocument;
			InitializeAsync();
		}

		public async void UpdateUI(ICharacterData data, IEnumerable<IItem> items, Action<IItem, int> buyClicked = null,
			Action<IItem, int> sellClicked = null, Action<IItem, int> useClicked = null)
		{
			await new WaitUntil(() => RootVisualElement != null);
			RootVisualElement.Find<Image>("ProfilePicture").image = data.ProfilePicture;
			RootVisualElement.Find<Label>("DialogueBox").text = data.DialogueText;
			UpdateItemList(items, buyClicked, sellClicked, useClicked);
		}

		private void UpdateItemList(IEnumerable<IItem> items, Action<IItem, int> buyClicked, 
			Action<IItem, int> sellClicked, Action<IItem, int> useClicked)
		{
			var itemListContainer = RootVisualElement.Find<VisualElement>("ItemList");
			itemListContainer.Clear();

			var groupedItems = items.GroupBy(i => i.Name).OrderBy(g => g.Key);
			foreach (var group in groupedItems)
			{
				var count = group.Count();
				var item = group.First();
				var slotsNeeded = (int)Math.Ceiling((double)count / item.MaxStackCount);

				for (var i = 0; i < slotsNeeded; i++)
				{
					var itemCount = item.MaxStackCount > 1 ? count : 1;
					var itemElement = CreateItemElement(item, itemCount);
					itemListContainer.Add(itemElement);

					if (buyClicked != null)
					{
						UIToolkitExtensions.CreateButton("Buy", "button", itemElement, OnBuyClicked);
						void OnBuyClicked() => buyClicked.Invoke(item, itemCount);
					}

					if (sellClicked != null && !item.Equipped)
					{
						UIToolkitExtensions.CreateButton("Sell", "button", itemElement, OnSellClicked);
						void OnSellClicked() => sellClicked.Invoke(item, itemCount);
					}

					if (useClicked != null && !item.Equipped)
					{
						UIToolkitExtensions.CreateButton("Use", "button", itemElement, OnUseClicked);
						void OnUseClicked() => useClicked.Invoke(item, itemCount);
					}
				}
			}
		}

		private VisualElement CreateItemElement(IItem item, int count)
		{
			var itemElement = UIToolkitExtensions.CreateVisualElement("item");
		    UIToolkitExtensions.CreateImage(item.Sprite.texture, "item-texture", itemElement);
		    UIToolkitExtensions.CreateLabel(item.Name, "item-name", itemElement);
		    UIToolkitExtensions.CreateLabel("x" + count, "item-count", itemElement);
		    UIToolkitExtensions.CreateLabel(item.Cost + "g", "item-cost", itemElement);
		    return itemElement;
	    }
	    
	    private async void InitializeAsync()
	    {
		    await new WaitUntil(() => RootVisualElement != null);
		    UIToolkitExtensions.CreateButton("X", "close-button", RootVisualElement, Hide);
		    Hide();
	    }
	}
}