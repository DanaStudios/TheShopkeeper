using System;
using System.Collections.Generic;
using System.Linq;
using AppData.Shopkeeper;
using Item;
using UnityEngine;
using UnityEngine.UIElements;

namespace Screens.Shop
{
	public class ShopScreen : IShopScreen
	{
		public bool Visible => RootVisualElement.style.display == DisplayStyle.Flex;
		public void Show() => RootVisualElement.style.display = DisplayStyle.Flex;
		public void Hide() => RootVisualElement.style.display = DisplayStyle.None;
		public event Action<IItem, int> BuyButtonPressed;
		private VisualElement RootVisualElement => UIDocument.rootVisualElement;
		private UIDocument UIDocument { get; }

		public ShopScreen(UIDocument uiDocument)
		{
			UIDocument = uiDocument;
			InitializeAsync();
		}

		public async void UpdateItemList(IShopkeeperData data, List<IItem> items)
		{
			await new WaitUntil(() => RootVisualElement != null);
			
			var profilePicture = FindVisualElement("ProfilePicture");
			if (profilePicture is Image image) image.image = data.ProfilePicture;
			
			var dialogueBox = FindVisualElement("DialogueBox");
			if (dialogueBox is Label label) label.text = data.DialogueText;
			
	        var itemListContainer = FindVisualElement("ItemList");
	        itemListContainer.Clear();
	        
	        var groupedItems = items.GroupBy(i => i.Name);
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
	                
	                var buyButton = CreateBuyButton();
	                buyButton.clicked += () => BuyButtonPressed?.Invoke(item, itemCount);
	                itemElement.Add(buyButton); 
	            }
	        }
	    }

		private VisualElement FindVisualElement(string selector) => RootVisualElement.Q<VisualElement>(selector);

		private VisualElement CreateItemElement(IItem item, int count)
	    {
		    var itemElement = new VisualElement();
		    itemElement.AddToClassList("item");
		    
		    var itemTexture = new Image();
		    itemTexture.image = item.Icon;
		    itemTexture.AddToClassList("item-texture");
		    itemElement.Add(itemTexture);
		    
		    var nameLabel = new Label(item.Name);
		    nameLabel.AddToClassList("item-name");
		    itemElement.Add(nameLabel);
		    
		    var itemCount = new Label("x" + count);
		    itemCount.AddToClassList("item-count");
		    itemElement.Add(itemCount);
		    
		    var itemCost = new Label(item.Cost + "g"); 
		    itemCost.AddToClassList("item-cost");
		    itemElement.Add(itemCost);

		    return itemElement;
	    }

	    private Button CreateBuyButton()
	    {
		    var buyButton = new Button
		    {
			    text = "Buy"
		    };
		    
		    buyButton.AddToClassList("buy-button");
		    return buyButton;
	    }
	    
	    private async void InitializeAsync()
	    {
		    await new WaitUntil(() => RootVisualElement != null);
		    CreateCloseButton();
		    Hide();
	    }

	    private void CreateCloseButton()
	    {
		    var closeButton = new Button
		    {
			    text = "X"
		    };
		    
		    closeButton.AddToClassList("close-button");
		    closeButton.clicked += Hide;
		    RootVisualElement.Add(closeButton);
	    }
	}
}