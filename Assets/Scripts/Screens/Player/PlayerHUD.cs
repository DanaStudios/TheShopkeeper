using AppData.Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace Screens.Player
{
	public class PlayerHUD : IPlayerHUD
	{
		public bool Visible => RootVisualElement.style.display == DisplayStyle.Flex;
		public void Show() => RootVisualElement.style.display = DisplayStyle.Flex;
		public void Hide() => RootVisualElement.style.display = DisplayStyle.None;
		private VisualElement RootVisualElement => UIDocument.rootVisualElement;
		private UIDocument UIDocument { get; }
		
		public PlayerHUD(UIDocument uiDocument, IPlayerData playerData)
		{
			UIDocument = uiDocument;
			InitializeAsync(playerData);
		}
		
		public async void UpdateGoldAmount(int amount)
		{
			await new WaitUntil(() => RootVisualElement != null);
			var amountElement = FindVisualElement("GoldLabel");
			if (amountElement is Label label) label.text = amount.ToString();
		}

		private VisualElement FindVisualElement(string selector) => RootVisualElement.Q<VisualElement>(selector);
		
		private async void InitializeAsync(IPlayerData playerData)
		{
			await new WaitUntil(() => RootVisualElement != null);

			var nameElement = FindVisualElement("PlayerName");
			if (nameElement is Label label) label.text = playerData.Name;
			
			var profilePictureElement = FindVisualElement("ProfilePicture");
			if (profilePictureElement is Image image) image.image = playerData.ProfilePicture;
			
			var goldElement = FindVisualElement("GoldLabel");
			if (goldElement is Label label1) label1.text = playerData.StartingGold.ToString();

			SetPickingModeRecursively(RootVisualElement, PickingMode.Ignore);
			
			Show();
		}
		
		void SetPickingModeRecursively(VisualElement element, PickingMode mode)
		{
			element.pickingMode = mode;
			foreach (var child in element.Children())
			{
				SetPickingModeRecursively(child, mode);
			}
		}
	}
}